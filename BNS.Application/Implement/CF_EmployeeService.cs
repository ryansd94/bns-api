using BNS.Application.Interface;
using BNS.Data.Entities;
using BNS.ViewModels;
using BNS.ViewModels.Requests;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BNS.Data.EntityContext;
using static BNS.Utilities.Enums;
using System.Data;
using BNS.Utilities;
using System.Net;
using BNS.ViewModels.Responses;
using AutoMapper;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Localization;
using BNS.Resource;
using BNS.Resource.LocalizationResources;

namespace BNS.Application.Implement
{
    public class CF_EmployeeService : GenericRepository<CF_Employee>, ICF_EmployeeService
    {
        private readonly IGenericRepository<CF_Employee> _genericRepository;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IMapper _mapper;
        private readonly MyConfiguration _myConfig;
        public CF_EmployeeService(BNSDbContext context,
            IGenericRepository<CF_Employee> genericRepository,
             IStringLocalizer<SharedResource> sharedLocalizer,
            IMapper mapper,
            IOptionsSnapshot<MyConfiguration> options) : base(context)
        {
            _genericRepository = genericRepository;
            _sharedLocalizer = sharedLocalizer;
            _mapper = mapper;
            //_myConfig = options.Value;
            _myConfig = new MyConfiguration();
        }
        public async Task<ApiResult<string>> Delete(List<Guid> ids)
        {
            var result = new ApiResult<string>();

            var emps = await _context.CF_Employees.Where(s => ids.Contains(s.Index)).ToListAsync();
            var accounts = await _context.CF_Accounts.Where(s => emps.Select(d => d.AccountIndex).ToList().Contains(s.Id)).ToListAsync();
            foreach (var item in emps)
            {
                item.IsDelete = true;
            }
            foreach (var item in accounts)
            {
                item.IsDelete = true;
            }
            await _context.SaveChangesAsync();


            return result;
        }

        public async Task<ApiResult<List<CF_EmployeeResponseModel>>> GetAllData(RequestPageModel<CF_EmployeeSearchModel> model)
        {
            var result = new ApiResult<List<CF_EmployeeResponseModel>>();

            result.status = HttpStatusCode.OK;
            var query = _context.CF_Employees.Where(s => !string.IsNullOrEmpty(s.EmployeeName) &&
            s.IsDelete == null &&
            s.ShopIndex == model.ShopIndex &&
            (s.CF_Account == null || (s.CF_Account != null && (s.CF_Account.IsMainAccount==null ||(s.CF_Account.IsMainAccount != null && !s.CF_Account.IsMainAccount.Value)))) &&
            s.CF_Branch.IsDelete == null &&
            (model.BranchIndexs == null || (model.BranchIndexs != null && model.BranchIndexs.Contains(s.BranchIndex.Value))))
                .Include(s => s.CF_Account)
                .Include(s => s.CF_Department)
                .Include(s => s.CF_Position)
                .Include(s => s.CF_Branch)
                .Select(s => new CF_EmployeeResponseModel
                {

                    EmployeeName = s.EmployeeName,
                    EmployeeCode = s.EmployeeCode,
                    Gender = s.Gender,
                    Nric = s.Nric,
                    DateOfNric = s.DateOfNric,
                    PlaceOfNric = s.PlaceOfNric,
                    BrithDate = s.BrithDate,
                    JoinedDate = s.JoinedDate,
                    PermanentAddress = s.PermanentAddress,
                    TemporaryAddress = s.TemporaryAddress,
                    Active = s.Active,
                    Email = s.Email,
                    Phone = s.Phone,
                    Id = s.Index,
                    DepartmentIndex = s.CF_Department != null && s.CF_Department.IsDelete == null ? s.CF_Department.Name : string.Empty,
                    PositionIndex = s.CF_Position != null && s.CF_Position.IsDelete == null ? s.CF_Position.Name : string.Empty,
                    UserName = s.CF_Account != null ? s.CF_Account.UserName : string.Empty,
                    CreatedDate = s.CreatedDate,
                    UpdatedDate = s.UpdatedDate,
                    WorkingDate = s.WorkingDate,
                    BranchName = s.CF_Branch.Name,
                    CreatedUserId = s.CreatedUser,
                    UpdatedUserId = s.UpdatedUser,
                    DepartmentIndexSearch = s.DepartmentIndex != null ? s.DepartmentIndex.Value.ToString() : string.Empty,
                    PositionIndexSearch = s.PositionIndex != null ? s.PositionIndex.Value.ToString() : string.Empty
                });

            if (model.filter != null)
            {
                if (!string.IsNullOrEmpty(model.filter.Keyword))
                {
                    query = query.Where(s => s.EmployeeName.ToLower().Equals(model.filter.Keyword.ToLower()) ||
                    s.EmployeeCode.ToLower().Equals(model.filter.Keyword.ToLower()) ||
                    s.Nric.ToLower().Equals(model.filter.Keyword.ToLower()));
                }
                if (model.filter.Gender != null && model.filter.Gender.Count > 0)
                {
                    query = query.Where(s => s.Gender != null && model.filter.Gender.Contains(s.Gender.Value.ToString()));
                }
                if (model.filter.StrDepartmentIndex != null && model.filter.StrDepartmentIndex.Count > 0)
                {
                    query = query.Where(s => !string.IsNullOrEmpty(s.DepartmentIndexSearch) && model.filter.StrDepartmentIndex.Contains(s.DepartmentIndexSearch));
                }
                if (model.filter.StrPositionIndex != null && model.filter.StrPositionIndex.Count > 0)
                {
                    query = query.Where(s => !string.IsNullOrEmpty(s.PositionIndexSearch) && model.filter.StrPositionIndex.Contains(s.PositionIndexSearch));
                }
                if (model.filter.WorkingDateFrom != null)
                {
                    query = query.Where(s => s.WorkingDate != null && s.WorkingDate.Value >= model.filter.WorkingDateFrom.Value.Date);
                }
                if (model.filter.WorkingDateTo != null)
                {
                    query = query.Where(s => s.WorkingDate != null && s.WorkingDate.Value <= model.filter.WorkingDateTo.Value.Date);
                }
            }
            if (model.columns != null)
            {
                var columnSort = model.columns[model.order[0].column].data;
                if (!string.IsNullOrEmpty(columnSort) && !model.isAdd && !model.isEdit)
                {
                    columnSort = columnSort[0].ToString().ToUpper() + columnSort.Substring(1, columnSort.Length - 1);
                    query = Common.OrderBy(query, columnSort, model.order[0].dir == ESortEnum.desc ? false : true);

                }
            }

            if (model.isAdd)
                query = query.OrderByDescending(s => s.CreatedDate);
            if (model.isEdit)
                query = query.OrderByDescending(s => s.UpdatedDate);

            if (model.search != null && !string.IsNullOrEmpty(model.search.value))
            {
                var valueSearch = model.search.value;
                query = Common.SearchBy(query, new CF_EmployeeResponseModel(), valueSearch);

            }

            result.recordsTotal = await query.CountAsync();
            result.recordsFiltered = result.recordsTotal;


            query = query.Skip(model.start).Take(model.length);

            var rs = await query.ToListAsync();
            var data = new List<CF_EmployeeResponseModel>();

            var depts = await _context.CF_Departments.Where(s => s.IsDelete == null &&
              s.ShopIndex == model.ShopIndex).ToListAsync();
            foreach (var item in rs)
            {
                if (!string.IsNullOrEmpty(item.EmployeeImage))
                    item.EmployeeImage = Ultility.GetPath(_myConfig.Default.FullDomain, model.ShopIndex, EUploadType.Employee) + item.EmployeeImage;

                //var emp = new CF_EmployeeResponseModel();
                //emp = _mapper.Map<CF_EmployeeResponseModel>(item);
                data.Add(item);
            }

            if (data.Count == 0)
            {
                result.data = new List<CF_EmployeeResponseModel>();
                result.draw = model.draw;
                result.recordsTotal = 0;
                result.recordsFiltered = 0;
                return result;
            }
            var accounts = await _context.CF_Accounts.Where(s => s.ShopIndex == model.ShopIndex).Include(s => s.Cf_Employee).ToListAsync();
            SetCreatedUpdatedUsername<CF_EmployeeResponseModel>(rs, accounts);

            result.data = data;
            result.draw = model.draw;

            return result;
        }

        public async Task<ApiResult<CF_EmployeeResponseModel>> GetByIndex(Guid id)
        {
            var result = new ApiResult<CF_EmployeeResponseModel>();

            var data = await _genericRepository.GetById(id);
            if (data != null)
            {
                result.data = _mapper.Map<CF_EmployeeResponseModel>(data);
                if (!string.IsNullOrEmpty(result.data.EmployeeImage))
                    result.data.EmployeeImage = Ultility.GetPath(_myConfig.Default.FullDomain, data.ShopIndex, EUploadType.Employee) + result.data.EmployeeImage;
                if (data.AccountIndex != null)
                {
                    var account = await _context.CF_Accounts.Where(s => s.Id == data.AccountIndex.Value).FirstOrDefaultAsync();
                    if (account != null)
                    {
                        result.data.UserName = account.UserName;
                        result.data.Password = account.PasswordHash;
                        result.data.ConfirmPassword = account.PasswordHash;
                    }
                }
            }
            return result;
        }
        private string UploadFile(Guid ShopIndex, string StrFiles)
        {
            if (!Directory.Exists(Ultility.GetPath(ShopIndex, EUploadType.Employee)))
                Directory.CreateDirectory(Ultility.GetPath(ShopIndex, EUploadType.Employee));



            byte[] bytes = Convert.FromBase64String(StrFiles);
            MemoryStream stream = new MemoryStream(bytes);
            var fileName = Path.GetRandomFileName() + ".png";
            IFormFile file = new FormFile(stream, 0, bytes.Length, fileName, fileName);


            if (file != null)
            {

                var filePath = Path.Combine(Ultility.GetPath(ShopIndex, EUploadType.Employee), fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }
            //if (model.Files != null)
            //{
            //    string webRootFolder = "wwwroot";
            //    string directory = $"/{Constants.__PATH_UPLOAD_FILE}/{model.ShopIndex }/{Constants.__PATH_EMPLOYEE_FILE}/";
            //    if (!Directory.Exists($"{webRootFolder}/{directory}"))
            //        Directory.CreateDirectory($"{webRootFolder}/{directory}");

            //    var xxxx = _myConfig.Default.FullDomain;
            //    var fileName = Path.GetRandomFileName();

            //    var filePath = Path.Combine($"{webRootFolder}/{directory}", fileName);
            //    using (var fileStream = new FileStream(filePath, FileMode.Create))
            //    {
            //        model.Files.CopyTo(fileStream);
            //    }


            //    emp.EmployeeImage = fileName;

            //}
            return fileName;
        }
        public async Task<ApiResult<string>> Save(CF_EmployeeModel model)
        {
            var result = new ApiResult<string>();


            var rs = 0;
            if (model.Index == Guid.Empty)
            {
                var emp = _mapper.Map<CF_Employee>(model);
                if (!string.IsNullOrEmpty(model.UserName))
                {
                    var accountCheck = _context.CF_Accounts.Where(s => s.ShopIndex == model.ShopIndex &&
                         s.UserName == model.UserName &&
                         s.IsDelete == null).FirstOrDefault();

                    if (accountCheck != null)
                    {
                        result.errorCode = EErrorCode.IsExistsData.ToString();
                        result.title = _sharedLocalizer[LocalizedBackendMessages.MSG_UserNameExistsData];
                        return result;
                    }
                }

                if (!string.IsNullOrEmpty(model.StrFiles))
                    emp.EmployeeImage = UploadFile(model.ShopIndex, model.StrFiles);




                emp.CreatedDate = DateTime.UtcNow;
                emp.CreatedUser = model.UserIndex;

                if (!string.IsNullOrEmpty(model.UserName))
                {
                    var user = new CF_Account()
                    {
                        UserName = model.UserName,
                        ShopIndex = model.ShopIndex,
                        IsMainAccount = false,
                        CreatedDate = DateTime.Now,
                        CreatedUser = model.UserIndex,
                        PasswordHash = Ultility.MD5Encrypt(model.Password)
                    };
                    _context.CF_Accounts.Add(user);
                    emp.AccountIndex = user.Id;
                }

                rs = await _genericRepository.Add(emp);
                result.type = EType.Add.ToString();
            }
            else
            {
                var emp = await _genericRepository.GetById(model.Index);
                CF_Account account = null;
                if (emp == null)
                {
                    result.errorCode = EErrorCode.NotExistsData.ToString();
                    result.title = _sharedLocalizer[LocalizedBackendMessages.MSG_NotExistsData];
                    return result;
                }
                if (!string.IsNullOrEmpty(model.UserName))
                {
                    account = _context.CF_Accounts.Where(s => s.ShopIndex == model.ShopIndex &&
                        s.UserName == model.UserName &&
                        s.IsDelete == null).FirstOrDefault();

                    if (account != null)
                    {
                        if (emp.AccountIndex != null && account.Id != emp.AccountIndex)
                        {
                            result.errorCode = EErrorCode.IsExistsData.ToString();
                            result.title = _sharedLocalizer[LocalizedBackendMessages.MSG_UserNameExistsData];
                            return result;
                        }
                    }
                }


                if (!string.IsNullOrEmpty(model.StrFiles))
                    emp.EmployeeImage = UploadFile(model.ShopIndex, model.StrFiles);


                emp.EmployeeName = model.EmployeeName;
                emp.EmployeeCode = model.EmployeeCode;
                emp.Gender = model.Gender;
                emp.Nric = model.Nric;
                emp.DateOfNric = model.DateOfNric;
                emp.BranchIndex = model.BranchIndex;
                emp.PlaceOfNric = model.PlaceOfNric;
                emp.BrithDate = model.BrithDate;
                emp.JoinedDate = model.JoinedDate;
                emp.PermanentAddress = model.PermanentAddress;
                emp.TemporaryAddress = model.TemporaryAddress;
                emp.Active = model.Active;
                emp.Email = model.Email;
                emp.Phone = model.Phone;
                emp.UpdatedUser = model.UserIndex;
                emp.UpdatedDate = DateTime.Now;
                emp.DepartmentIndex = model.DepartmentIndex;
                emp.PositionIndex = model.PositionIndex;
                emp.WorkingDate = model.WorkingDate;
                if (!string.IsNullOrEmpty(model.UserName))
                {
                    var isAddNewAccount = false;
                    if (account == null)
                    {
                        isAddNewAccount = true;
                        account = new CF_Account();
                        account.CreatedDate = DateTime.UtcNow;
                        account.CreatedUser = model.UserIndex;
                    }
                    else
                    {
                        account.UpdatedUser = model.UserIndex;
                        account.UpdatedDate = DateTime.UtcNow;
                    }
                    account.ShopIndex = model.ShopIndex;
                    account.UserName = model.UserName;
                    if (isAddNewAccount)
                    {
                        account.PasswordHash = Ultility.MD5Encrypt(model.Password);
                        _context.CF_Accounts.Add(account);
                        emp.AccountIndex = account.Id;
                    }
                    else
                    {
                        if (model.Password != account.PasswordHash)
                            account.PasswordHash = Ultility.MD5Encrypt(model.Password);
                        _context.CF_Accounts.Update(account);
                    }
                }

                rs = await _genericRepository.Update(emp);
                result.type = EType.Edit.ToString();
            }
            if (rs <= 0)
                result.errorCode = EErrorCode.USERNAME_ISEXISTS.ToString();

            return result;
        }
    }
}
