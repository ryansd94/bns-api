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
using AutoMapper;
using BNS.Resource;
using Microsoft.Extensions.Localization;
using BNS.Resource.LocalizationResources;
using Newtonsoft.Json;
using BNS.ViewModels.Responses;

namespace BNS.Application.Implement
{
    public class Sys_RoleService : GenericRepository<Sys_Role>,
        ISys_RoleService
    {
        private readonly IGenericRepository<Sys_Role> _genericRepository;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IMapper _mapper;
        public Sys_RoleService(BNSDbContext context,
            IGenericRepository<Sys_Role> genericRepository,
             IStringLocalizer<SharedResource> sharedLocalizer,
             IMapper mapper) : base(context)
        {
            _genericRepository = genericRepository;
            _sharedLocalizer = sharedLocalizer;
            _mapper = mapper;
        }


        public async Task<ApiResult<List<Sys_RoleResponseModel>>> GetAllData(RequestPageModel<Sys_RoleResponseModel> model)
        {
            var result = new ApiResult<List<Sys_RoleResponseModel>>();

            result.status = HttpStatusCode.OK;

            var users = await _context.CF_Accounts.Where(s => s.ShopIndex == model.ShopIndex).ToListAsync();

            var query = _context.Sys_Roles.Where(s => !string.IsNullOrEmpty(s.Name) &&
            s.IsDelete == null &&
            s.ShopIndex == model.ShopIndex)
                .Select(s => new Sys_RoleResponseModel
                {
                    Name = s.Name,
                    Note = s.Description,
                    Id = s.Id,
                    UpdatedDate = s.UpdatedDate,
                    CreatedDate = s.CreatedDate,
                    CreatedUserId = s.CreatedUser,
                    UpdatedUserId = s.UpdatedUser
                });
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
                query = Common.SearchBy(query, new Sys_RoleResponseModel(), valueSearch);

            }
            result.recordsTotal = await query.CountAsync();
            result.recordsFiltered = result.recordsTotal;


            query = query.Skip(model.start).Take(model.length);

            var rs = await query.ToListAsync();

            if (rs.Count == 0)
            {
                result.data = new List<Sys_RoleResponseModel>();
                result.draw = model.draw;
                result.recordsTotal = 0;
                result.recordsFiltered = 0;
                return result;
            }
            var accounts = await _context.CF_Accounts.Where(s => s.ShopIndex == model.ShopIndex).Include(s => s.Cf_Employee).ToListAsync();
            SetCreatedUpdatedUsername<Sys_RoleResponseModel>(rs, accounts);
            result.data = rs;
            result.draw = model.draw;

            return result;
        }

        public async Task<ApiResult<string>> Save(Sys_RoleModel model)
        {

            var result = new ApiResult<string>();

            var rs = 0;
            if (model.Index == Guid.Empty)
            {
                var checkIsExists = _context.Sys_Roles.Where(s => s.Name.Equals(model.Name) &&
                s.ShopIndex == model.ShopIndex &&
                s.IsDelete == null).FirstOrDefault();
                if (checkIsExists != null)
                {
                    result.errorCode = EErrorCode.IsExistsData.ToString();
                    result.title = _sharedLocalizer[LocalizedBackendMessages.MSG_ExistsData];
                    return result;
                }
                await _genericRepository.AddAsync(new Sys_Role
                {
                    Name = model.Name,
                    Description = model.Note,
                    CreatedDate = DateTime.UtcNow,
                    ShopIndex = model.ShopIndex,
                    CreatedUser = model.UserIndex
                });

                result.type = EType.Add.ToString();
            }
            else
            {
                var dataCheck = _context.Sys_Roles.Where(s => s.Name.ToLower() == model.Name.ToLower() &&
                s.Id != model.Index &&
                s.ShopIndex == model.ShopIndex &&
                s.IsDelete == null).FirstOrDefault();
                if (dataCheck != null)
                {
                    result.errorCode = EErrorCode.IsExistsData.ToString();
                    result.title = _sharedLocalizer[LocalizedBackendMessages.MSG_ExistsData];
                    return result;
                }
                var dataSave = await _genericRepository.GetByIdAsync(model.Index);
                if (dataSave == null)
                {
                    result.errorCode = EErrorCode.NotExistsData.ToString();
                    result.title = _sharedLocalizer[LocalizedBackendMessages.MSG_NotExistsData];
                    return result;
                }
                dataSave.Name = model.Name;
                dataSave.Description = model.Note;
                dataSave.UpdatedDate = DateTime.UtcNow;
                dataSave.UpdatedUser = model.UserIndex;
                await _genericRepository.UpdateAsync(dataSave);
                result.type = EType.Edit.ToString();
            }
            if (rs <= 0)
                result.errorCode = EErrorCode.USERNAME_ISEXISTS.ToString();

            return result;

        }

        public async Task<ApiResult<Sys_RoleResponseModel>> GetByIndex(Guid id)
        {

            var result = new ApiResult<Sys_RoleResponseModel>();

            var data = await _genericRepository.GetByIdAsync(id);
            result.data = new Sys_RoleResponseModel();
            result.data.Id = data.Id;
            result.data.Name = data.Name;
            result.data.Note = data.Description;
            result.data.Permission = data.Permission;

            return result;
        }

        public async Task<ApiResult<string>> Delete(List<Guid> ids)
        {
            var result = new ApiResult<string>();
            var data = await _context.Sys_Roles.Where(s => ids.Contains(s.Id)).ToListAsync();
            foreach (var item in data)
            {
                item.IsDelete = true;
            }
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task<ApiResult<string>> Decentralize(Sys_DecentralizeModel model)
        {
            var result = new ApiResult<string>();
            var role = await _context.Sys_Roles.Where(s => s.Id == model.RoleId && s.IsDelete == null).FirstOrDefaultAsync();
            if (role == null)
            {
                result.errorCode = EErrorCode.IsExistsData.ToString();
                result.title = _sharedLocalizer[LocalizedBackendMessages.MSG_NotExistsData];
                return result;
            }
            role.Permission = JsonConvert.SerializeObject(model.LstKeyRole);
            await _context.SaveChangesAsync();
            return result;
        }

        public Task<ApiResult<List<Sys_RoleModel>>> GetRole(Guid id, ERoleType type)
        {
            throw new NotImplementedException();
        }
    }
}
