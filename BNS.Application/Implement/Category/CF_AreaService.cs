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

namespace BNS.Application.Implement
{
    public class CF_AreaService : GenericRepository<CF_Area>,
        ICF_AreaService
    {
        private readonly IGenericRepository<CF_Area> _genericRepository;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IMapper _mapper;
        public CF_AreaService(BNSDbContext context,
            IGenericRepository<CF_Area> genericRepository,
             IStringLocalizer<SharedResource> sharedLocalizer,
             IMapper mapper) : base(context)
        {
            _genericRepository = genericRepository;
            _sharedLocalizer = sharedLocalizer;
            _mapper = mapper;
        }


        public async Task<ApiResult<List<CategoryResponseModel>>> GetAllData(RequestPageModel<CategoryResponseModel> model)
        {
            var result = new ApiResult<List<CategoryResponseModel>>();

            result.status = HttpStatusCode.OK;

            var users = await _context.CF_Accounts.Where(s => s.ShopIndex == model.ShopIndex).ToListAsync();

            var query = _context.CF_Areas.Where(s => !string.IsNullOrEmpty(s.Name) &&
            s.IsDelete == null &&
            s.ShopIndex == model.ShopIndex &&
            s.CF_Branch.IsDelete == null &&
        (model.BranchIndexs == null || (model.BranchIndexs != null && model.BranchIndexs.Contains(s.BranchIndex.Value))))
                .Include(s => s.CF_Branch)
                .Select(s => new CategoryResponseModel
                {
                    Name = s.Name,
                    Note = s.Description,
                    Number = s.Number,
                    Id = s.Index,
                    UpdatedDate = s.UpdatedDate,
                    CreatedDate = s.CreatedDate,
                    CreatedUserId = s.CreatedUser,
                    UpdatedUserId = s.UpdatedUser,
                    BranchName = s.CF_Branch.Name
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
            if (model.sortModel != null && model.sortModel.Count > 0)
            {
                var columnSort = model.sortModel[0].field;
                var sortType = model.sortModel[0].sort;
                if (!string.IsNullOrEmpty(columnSort) && !model.isAdd && !model.isEdit)
                {
                    columnSort = columnSort[0].ToString().ToUpper() + columnSort.Substring(1, columnSort.Length - 1);
                    query = Common.OrderBy(query, columnSort, sortType == ESortEnum.desc.ToString() ? false : true);

                }
            }
            if (model.isAdd)
                query = query.OrderByDescending(s => s.CreatedDate);
            if (model.isEdit)
                query = query.OrderByDescending(s => s.UpdatedDate);

            if (model.search != null && !string.IsNullOrEmpty(model.search.value))
            {
                var valueSearch = model.search.value;
                query = Common.SearchBy(query, new CategoryResponseModel(), valueSearch);

            }
            result.recordsTotal = await query.CountAsync();
            result.recordsFiltered = result.recordsTotal;


            query = query.Skip(model.start).Take(model.length);

            var rs = await query.ToListAsync();

            if (rs.Count == 0)
            {
                result.data = new List<CategoryResponseModel>();
                result.draw = model.draw;
                result.recordsTotal = 0;
                result.recordsFiltered = 0;
                return result;
            }
            var accounts = await _context.CF_Accounts.Where(s => s.ShopIndex == model.ShopIndex).Include(s => s.Cf_Employee).ToListAsync();
            SetCreatedUpdatedUsername<CategoryResponseModel>(rs, accounts);
            result.data = rs;
            result.draw = model.draw;

            return result;
        }

        public async Task<ApiResult<string>> Save(CF_AreaModel model)
        {

            var result = new ApiResult<string>();

            var rs = 0;
            if (model.Index == Guid.Empty)
            {
                var checkIsExists = _context.CF_Areas.Where(s => s.Name.ToLower() == model.Name.ToLower() &&
                s.BranchIndex == model.BranchIndex &&
                s.ShopIndex == model.ShopIndex &&
                s.IsDelete == null).FirstOrDefault();
                if (checkIsExists != null)
                {
                    result.errorCode = EErrorCode.IsExistsData.ToString();
                    result.title = _sharedLocalizer[LocalizedBackendMessages.MSG_ExistsData];
                    return result;
                }
                rs = await _genericRepository.Add(new CF_Area
                {
                    Name = model.Name,
                    Description = model.Note,
                    Number = model.Number,
                    CreatedDate = DateTime.UtcNow,
                    ShopIndex = model.ShopIndex,
                    BranchIndex = model.BranchIndex,
                    CreatedUser = model.UserIndex
                });

                result.type = EType.Add.ToString();
            }
            else
            {
                var areaCheck = _context.CF_Areas.Where(s => s.Name.ToLower() == model.Name.ToLower() &&
                s.Index != model.Index &&
                s.ShopIndex == model.ShopIndex &&
                s.IsDelete == null).FirstOrDefault();
                if (areaCheck != null)
                {
                    result.errorCode = EErrorCode.IsExistsData.ToString();
                    result.title = _sharedLocalizer[LocalizedBackendMessages.MSG_ExistsData];
                    return result;
                }
                var area = await _genericRepository.GetById(model.Index);
                if (area == null)
                {
                    result.errorCode = EErrorCode.NotExistsData.ToString();
                    result.title = _sharedLocalizer[LocalizedBackendMessages.MSG_NotExistsData];
                    return result;
                }
                area.Name = model.Name;
                area.Description = model.Note;
                area.Number = model.Number;
                area.BranchIndex = model.BranchIndex;
                area.UpdatedDate = DateTime.UtcNow;
                area.UpdatedUser = model.UserIndex;
                rs = await _genericRepository.Update(area);
                result.type = EType.Edit.ToString();
            }
            if (rs <= 0)
                result.errorCode = EErrorCode.USERNAME_ISEXISTS.ToString();

            return result;

        }

        public async Task<ApiResult<CategoryResponseModel>> GetByIndex(Guid id)
        {

            var result = new ApiResult<CategoryResponseModel>();

            var data = await _genericRepository.GetById(id);
            result.data = new CategoryResponseModel();
            result.data.Id = data.Index;
            result.data.Name = data.Name;
            result.data.Note = data.Description;
            result.data.Number = data.Number;
            result.data.BranchIndex = data.BranchIndex;

            return result;
        }

        public async Task<ApiResult<string>> Delete(List<Guid> ids)
        {
            var result = new ApiResult<string>();
            var data = await _context.CF_Areas.Where(s => ids.Contains(s.Index)).ToListAsync();
            foreach (var item in data)
            {
                item.IsDelete = true;
            }
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task<ApiResult<int>> GetMaxNumber(Guid shopIndex, Guid? branchIndex)
        {
            var result = new ApiResult<int>();
            result.data = await GetMaxNumber<CF_Area>(shopIndex, branchIndex);
            return result;
        }
    }
}
