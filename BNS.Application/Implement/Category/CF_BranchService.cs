using BNS.Application.Interface;
using BNS.Data.Entities;
using BNS.ViewModels;
using BNS.ViewModels.Requests;
using BNS.ViewModels.Responses.Category;
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

namespace BNS.Application.Implement
{
    public class CF_BranchService : GenericRepository<CF_Branch>,
        ICF_BranchService
    {
        private readonly IGenericRepository<CF_Branch> _genericRepository;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IMapper _mapper;
        public CF_BranchService(BNSDbContext context,
            IGenericRepository<CF_Branch> genericRepository,
             IStringLocalizer<SharedResource> sharedLocalizer,
             IMapper mapper) : base(context)
        {
            _genericRepository = genericRepository;
            _sharedLocalizer = sharedLocalizer;
            _mapper = mapper;
        }

        public async Task<ApiResult<List<CF_BranchDefaultResponseModel>>> GetByUserId(Guid UserId,
            Guid ShopIndex)
        {
            var result = new ApiResult<List<CF_BranchDefaultResponseModel>>();
            result.status = HttpStatusCode.OK;

            var user = await _context.CF_Accounts.Where(s => s.Id == UserId).FirstOrDefaultAsync();
            if (user == null)
            {
                return result;
            }
            if (user.IsMainAccount == null || !user.IsMainAccount.Value)
            {
                return result;
            }
            var query = _context.CF_Branches.Where(s => !string.IsNullOrEmpty(s.Name) &&
            s.IsDelete == null &&
            s.ShopIndex == ShopIndex)
                .Select(s => new CF_BranchDefaultResponseModel
                {
                    Name = s.Name,
                    Index = s.Index,
                    Checked = false
                });

            var rs = await query.ToListAsync();
            if (!string.IsNullOrEmpty(user.BranchDefault))
            {
                var branchs = JsonConvert.DeserializeObject<List<string>>(user.BranchDefault);
                foreach (var item in rs)
                {
                    if (branchs.Contains(item.Index.ToString()))
                        item.Checked = true;
                }
            }
            result.data = rs;
            return result;
        }
        public async Task<ApiResult<List<CF_BranchResponseModel>>> GetAllData(RequestPageModel<CF_BranchResponseModel> model)
        {
            var result = new ApiResult<List<CF_BranchResponseModel>>();

            result.status = HttpStatusCode.OK;
            var query = _context.CF_Branches.Where(s => !string.IsNullOrEmpty(s.Name) &&
            s.IsDelete == null &&
            s.ShopIndex == model.ShopIndex)
                .Select(s => new CF_BranchResponseModel
                {
                    Name = s.Name,
                    Note = s.Description,
                    Number = s.Number,
                    Id = s.Index,
                    Address = s.Address,
                    Phone = s.Phone,
                    UpdatedDate = s.UpdatedDate,
                    CreatedDate = s.CreatedDate
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
                query = Common.SearchBy(query, new CF_BranchResponseModel(), valueSearch);

            }
            result.recordsTotal = await query.CountAsync();
            result.recordsFiltered = result.recordsTotal;


            query = query.Skip(model.start).Take(model.length);

            var rs = await query.ToListAsync();

            if (rs.Count == 0)
            {
                result.data = new List<CF_BranchResponseModel>();
                result.draw = model.draw;
                result.recordsTotal = 0;
                result.recordsFiltered = 0;
                return result;
            }

            var accounts = await _context.CF_Accounts.Where(s => s.ShopIndex == model.ShopIndex).Include(s => s.Cf_Employee).ToListAsync();
            SetCreatedUpdatedUsername<CF_BranchResponseModel>(rs, accounts);
            result.data = rs;
            result.draw = model.draw;

            return result;
        }

        public async Task<ApiResult<string>> Save(CF_BranchModel model)
        {
            var result = new ApiResult<string>();
            var rs = 0;
            if (model.Index == Guid.Empty)
            {
                var checkIsExists = _context.CF_Branches.Where(s => s.Name.ToLower() == model.Name.ToLower() &&
                s.ShopIndex == model.ShopIndex &&
                s.IsDelete == null).FirstOrDefault();
                if (checkIsExists != null)
                {
                    result.errorCode = EErrorCode.IsExistsData.ToString();
                    result.title = _sharedLocalizer[LocalizedBackendMessages.MSG_ExistsData];
                    return result;
                }
                var save = _mapper.Map<CF_Branch>(model);
                save.CreatedDate = DateTime.UtcNow;
                save.CreatedUser = model.UserIndex;
                rs = await _genericRepository.Add(save);
                result.type = EType.Add.ToString();
            }
            else
            {
                var dataCheck = _context.CF_Branches.Where(s => s.Name.ToLower() == model.Name.ToLower() &&
                s.Index != model.Index &&
                s.ShopIndex == model.ShopIndex &&
                s.IsDelete == null).FirstOrDefault();
                if (dataCheck != null)
                {
                    result.errorCode = EErrorCode.IsExistsData.ToString();
                    result.title = _sharedLocalizer[LocalizedBackendMessages.MSG_ExistsData];
                    return result;
                }
                var data = await _genericRepository.GetById(model.Index);
                if (data == null)
                {
                    result.errorCode = EErrorCode.NotExistsData.ToString();
                    result.title = _sharedLocalizer[LocalizedBackendMessages.MSG_NotExistsData];
                    return result;
                }
                data.Name = model.Name;
                data.Address = model.Address;
                data.Phone = model.Phone;
                data.Description = model.Note;
                data.Number = model.Number;
                data.UpdatedDate = DateTime.UtcNow;
                data.UpdatedUser = model.UserIndex;
                rs = await _genericRepository.Update(data);
                result.type = EType.Edit.ToString();
            }
            if (rs <= 0)
                result.errorCode = EErrorCode.USERNAME_ISEXISTS.ToString();

            return result;

        }

        public async Task<ApiResult<CF_BranchResponseModel>> GetByIndex(Guid id)
        {
            var result = new ApiResult<CF_BranchResponseModel>();
            var data = await _genericRepository.GetById(id);
            result.data = _mapper.Map<CF_BranchResponseModel>(data);
            result.data.Note = data.Description;
            return result;
        }

        public async Task<ApiResult<string>> Delete(List<Guid> ids)
        {
            var result = new ApiResult<string>();
            var data = await _context.CF_Branches.Where(s => ids.Contains(s.Index)).ToListAsync();
            if (data.Any(s => s.IsMaster != null && s.IsMaster.Value))
            {
                result.errorCode = EErrorCode.Failed.ToString();
                result.title = _sharedLocalizer[LocalizedBackendMessages.MSG_NotDeleteMasterBranch];
                return result;
            }
            foreach (var item in data)
            {
                item.IsDelete = true;
            }
            await _context.SaveChangesAsync();

            return result;
        }

        public async Task<ApiResult<int>> GetMaxNumber(Guid shopIndex)
        {
            var result = new ApiResult<int>();
            result.data = await GetMaxNumber<CF_Branch>(shopIndex,null);
            return result;
        }

    }
}
