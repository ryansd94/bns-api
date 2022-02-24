using BNS.Application.Interface;
using BNS.Data.Entities;
using BNS.Data.EntityContext;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using BNS.ViewModels.Requests;
using BNS.ViewModels.Responses;
using BNS.ViewModels;
using static BNS.Utilities.Enums;
using BNS.Utilities;
using BNS.Utilities.Interface;
using BNS.Application.Exceptions;
using System.Collections.Generic;
using Microsoft.Extensions.Localization;
using BNS.Resource;
using BNS.Resource.LocalizationResources;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;
using BNS.ViewModels.Responses.Menu;
using Microsoft.AspNetCore.Hosting;
using BNS.Utilities.Constant;

namespace BNS.Application.Implement
{
    public class CF_AccountService : GenericRepository<CF_Account>, ICF_AccountService
    {
        private readonly UserManager<CF_Account> _userManager;
        private readonly SignInManager<CF_Account> _signInManager;

        private readonly ICacheData _cache;
        private readonly RoleManager<Sys_Role> _roleManager;

        private readonly MyConfiguration _config;
        private readonly ISYS_ControlService _sys_ControlService;
        private readonly ICaptcha _captcha;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IHostingEnvironment _hostingEnvironment;
        public CF_AccountService(BNSDbContext context, UserManager<CF_Account> userManager,
            SignInManager<CF_Account> signInManager, 
             RoleManager<Sys_Role> roleManager,
             IStringLocalizer<SharedResource> sharedLocalizer,
             IOptions<MyConfiguration> config,
              ICaptcha captcha,
              ICacheData cache,
          ISYS_ControlService sys_ControlService,
           IHostingEnvironment hostingEnvironment,
        IOptionsSnapshot<MyConfiguration> options) : base(context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            //_config = config.Value;
            _config = new MyConfiguration();
            _roleManager = roleManager;
            _captcha = captcha;
            _sharedLocalizer = sharedLocalizer;
            _sys_ControlService = sys_ControlService;
            _cache = cache;
            _hostingEnvironment = hostingEnvironment;

        }
        public async Task<ApiResult<CF_AccountLoginResponseModel>> Authenticate(CF_AccountLoginModel request)
        {
            var rs = new ApiResult<CF_AccountLoginResponseModel>();
            try
            {
                //var user = await (_context.CF_Accounts.Where(s => s.UserName == request.UserName && s.ShopCode == request.ShopCode).FirstOrDefaultAsync());


                var shop = await _context.CF_Shops.Where(s => s.Code == request.ShopCode).FirstOrDefaultAsync();
                if (shop == null)
                {
                    rs.errorCode = EErrorCode.Failed.ToString();
                    rs.title = _sharedLocalizer[LocalizedBackendMessages.MSG_ShopCodeIsNotExists];
                    return rs;
                }
                var user = await _context.CF_Accounts
                    .Where(s => s.IsDelete == null && s.UserName == request.UserName &&
                    s.PasswordHash == Ultility.MD5Encrypt(request.Password))
                    .Where(s => s.CF_Shop.Code == request.ShopCode)
                    .Include(s => s.CF_Shop)
                    .FirstOrDefaultAsync();
                if (user == null)
                {
                    rs.errorCode = EErrorCode.Failed.ToString();
                    rs.title = _sharedLocalizer[LocalizedBackendMessages.MSG_UsernameOrPasswordNotCorrect];
                    return rs;
                }
                //check expried date
                if (shop.ToDate <= DateTime.Now)
                {
                    rs.errorCode = EErrorCode.Failed.ToString();
                    rs.title = _sharedLocalizer[LocalizedBackendMessages.MSG_UsernameOrPasswordNotCorrect];
                    return rs;
                }
                rs.data = new CF_AccountLoginResponseModel();

                var branch = await _context.CF_Branches.Where(s => s.ShopIndex == shop.Index).FirstOrDefaultAsync();
                string keyMenu = Ultility.GetCacheKey(EDataType.Menu, shop.Index, null);
                //Get menu 
                var menus = new List<MenuResponseModel>();
                if (shop.VersionType == null)
                {
                    var menuData = _cache.GetToCache(keyMenu);
                    if (menuData != null)
                    {
                        menus = (List<MenuResponseModel>)menuData;
                    }
                    else
                    {
                        Ultility.GetJsonFile<List<MenuResponseModel>>(ref menus, _hostingEnvironment.ContentRootPath + Constants.__PATH_MENU + "root.json");
                        foreach (var item in menus)
                        {
                            if (!string.IsNullOrEmpty(item.header))
                                item.header = _sharedLocalizer[item.header];
                            if (item.childPage != null && item.childPage.Count > 0)
                            {
                                foreach (var child in item.childPage)
                                {
                                    if (!string.IsNullOrEmpty(child.header))
                                        child.header = _sharedLocalizer[child.header];
                                    if (child.button != null && child.button.Count > 0)
                                    {
                                        foreach (var button in child.button)
                                        {
                                            if (!string.IsNullOrEmpty(button.text))
                                                button.text = _sharedLocalizer[button.text];

                                        }
                                    }
                                }
                            }
                            if (item.button != null && item.button.Count > 0)
                            {
                                foreach (var button in item.button)
                                {
                                    if (!string.IsNullOrEmpty(button.text))
                                        button.text = _sharedLocalizer[button.text];

                                }
                            }
                        }
                        _cache.AddToCache(keyMenu, menus);
                    }

                }
                //var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.Remember, true);

                //if (!result.Succeeded)
                //    return null;

                //var roles = _userManager.GetRolesAsync(user);
                var roles = new List<string>();
                if (user.IsMainAccount != null && user.IsMainAccount.Value)
                    roles.Add(EAccountType.SupperAdmin.ToString());
                else
                    roles.Add(EAccountType.User.ToString());
                var claims = new[]
                {
                new Claim(ClaimTypes.GivenName, user.UserName),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("UserId", user.Id.ToString()),
                new Claim("ShopIndex", shop.Index.ToString()),
                new Claim("Role",string.Join(";",roles))
            };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.Tokens.Key));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(_config.Tokens.Issuer
                    , _config.Tokens.Issuer
                    , claims
                    , expires: DateTime.UtcNow.AddDays(1)
                    , signingCredentials: creds
                    );
                var columnControl = await _sys_ControlService.GetColumnControl(shop.Index);
                var data = new JwtSecurityTokenHandler().WriteToken(token);
                rs.data.Token = data;
                rs.data.ColumnControls = columnControl.data;
                rs.data.Menus = menus;
                rs.data.ShopIndex = shop.Index;
                rs.data.UserId = user.Id.ToString();
                rs.data.BranchIndex = branch?.Index;
                rs.data.Branchs = !string.IsNullOrEmpty(user.BranchDefault) ? JsonConvert.DeserializeObject<List<Guid>>(user.BranchDefault) : null;
                rs.data.MainAccount = user.IsMainAccount != null ? user.IsMainAccount.Value : false;
            }
            catch (Exception ex)
            {
                rs.errorCode = EErrorCode.Failed.ToString();
                rs.title = ex.ToString();
                return rs;
            }
            return rs;
        }

        public ApiResult<CaptChaModel> GetCaptcha()
        {
            var rs = new ApiResult<CaptChaModel>();
            var captcha = new Random().Next(1000, 9999).ToString();
            rs.data = new CaptChaModel();
            rs.data.CaptchaValue = captcha;
            rs.data.CaptchaCode = StringCipher.Encrypt(captcha);
            rs.data.Captcha = _captcha.GenerateImage(captcha, 100, 48, "Arial");
            return rs;
        }
        public async Task<ApiResult<string>> Register(CF_AccountRegisterModel request)
        {
            var shopExists = await _context.CF_Shops.Where(s => s.Code.ToLower() == request.ShopCode.ToLower()).FirstOrDefaultAsync();
            if (shopExists != null)
            {
                throw new AppException(_sharedLocalizer[LocalizedBackendMessages.MSG_ShopCodeIsExists]);
                //rs.errorCode = EErrorCode.IsExistsData.ToString();
                //rs.message = _lang.GetJSONString("MSG_ShopCodeIsExists", request.lang);
                //return rs;
            }

            var rs = new ApiResult<string>();

            var shop = new CF_Shop
            {
                Name = request.ShopName,
                Code = request.ShopCode,
                Address = request.Address,
                Phone = request.Phone,
                FromDate = DateTime.UtcNow,
                ToDate = DateTime.UtcNow.AddDays(_config.Default.NumberOfTrialDay)
            };
            #region Insert CF_Shop
            var _shop = _context.CF_Shops.Add(shop);
            #endregion

            #region Insert CF_Account
            var shopIndex = shop.Index;
            var user = new CF_Account()
            {
                UserName = request.UserName,
                ShopCode = request.ShopCode,
                ShopIndex = shopIndex,
                IsMainAccount = true,
                CreatedDate = DateTime.UtcNow,
                PasswordHash = Ultility.MD5Encrypt(request.Password)
            };
            _context.CF_Accounts.Add(user);
            #endregion

            #region Insert CF_Employee
            var employee = new CF_Employee()
            {
                AccountIndex = user.Id,
                EmployeeName = request.UserName,
                IsMainAccount = true,
                ShopIndex = shopIndex
            };
            _context.CF_Employees.Add(employee);
            #endregion

            #region Insert CF_Branch
            var branch = new CF_Branch
            {
                ShopIndex = shopIndex,
                Name = _sharedLocalizer[LocalizedBackendMessages.BranchDefault],
                Address = request.Address,
                Phone = request.Phone,
                CreatedDate = DateTime.UtcNow,
                CreatedUser = user.Id,
                IsMaster = true
            };
            _context.CF_Branches.Add(branch);
            #endregion

            #region Insert CF_ShopSetting
            _context.CfShopSettings.Add(new CF_ShopSetting
            {
                ShopIndex = shopIndex,
                BranchIndex = branch.Index,
                IsGoiMonKhiHetTonKho = true,
                IsThanhToanCacMonDaOrder = true
            });
            #endregion

            await _context.SaveChangesAsync();

            rs.title = _sharedLocalizer[LocalizedBackendMessages.MSG_RegisterSuccess];
            return rs;

        }
        public async Task<ApiResult<List<string>>> GetBranchDefault(Guid UserIndex)
        {
            var result = new ApiResult<List<string>>();
            var account = await _context.CF_Accounts.Where(s => s.Id == UserIndex).FirstOrDefaultAsync();
            if (account == null)
            {
                result.errorCode = EErrorCode.Failed.ToString();
                result.title = _sharedLocalizer[LocalizedBackendMessages.User.MSG_NotExistsUser];
                return result;
            }
            result.data = !string.IsNullOrEmpty(account.BranchDefault) ? JsonConvert.DeserializeObject<List<string>>(account.BranchDefault) : null;
            return result;
        }

        public async Task<ApiResult<string>> SaveBranchDefault(CF_AccountUpdateBranchModel model)
        {
            var result = new ApiResult<string>();
            var account = await _context.CF_Accounts.Where(s => s.Id == model.UserIndex).FirstOrDefaultAsync();
            if (account == null)
            {
                result.errorCode = EErrorCode.Failed.ToString();
                result.title = _sharedLocalizer[LocalizedBackendMessages.User.MSG_NotExistsUser];
                return result;
            }
            account.BranchDefault = JsonConvert.SerializeObject(model.Branchs);
            await _context.SaveChangesAsync();
            return result;
        }
    }
}
