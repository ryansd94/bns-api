
using BNS.Data.Entities.JM_Entities;
using BNS.Domain;
using BNS.Utilities;
using BNS.Domain.Responses;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using BNS.Domain.Queries;
using AutoMapper;
using System.Threading.Tasks;
using System.Collections.Generic;
using static BNS.Utilities.Enums;
using System.Threading;
using System.Linq.Expressions;
using System;

namespace BNS.Service.Features
{
    public class GetTeamQuery : GetRequestHandler<TeamResponseItem, JM_Team, GetTeamRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetTeamQuery(
           IMapper mapper,
           IUnitOfWork unitOfWork) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public override IQueryable<JM_Team> GetQueryableData(GetTeamRequest request)
        {
            var query = _unitOfWork.Repository<JM_Team>()
               .Include(s => s.Childs)
               .ThenInclude(s => s.Childs)
               .ThenInclude(s => s.Childs)
               .ThenInclude(s => s.Childs)
               .OrderBy(d => d.CreatedDate)
               .Where(s => !s.IsDelete && s.CompanyId == request.CompanyId &&
               (request.IsParentChild == false || (request.IsParentChild != false && !s.ParentId.HasValue)));

            return query;
        }

        public override async Task<ApiResultList<TeamResponseItem>> Handle(GetTeamRequest request, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(request.filters))
            {
                return await GetDataByFilter(request);
            }
            else
            {
                return await base.Handle(request, cancellationToken);
            }
        }

        private async Task<ApiResultList<TeamResponseItem>> GetDataByFilter(GetTeamRequest request)
        {
            var response = new ApiResultList<TeamResponseItem>();
            response.data = new DynamicDataItem<TeamResponseItem>();
            var queryGroup = _unitOfWork.Repository<JM_Team>()
               .Where(s => !s.IsDelete && s.CompanyId == request.CompanyId &&
               !s.ParentId.HasValue);

            queryGroup = queryGroup.WhereOr(request.filters, request.defaultFilters);

            var query = _unitOfWork.Repository<JM_Team>()
               .Where(s => !s.IsDelete && s.CompanyId == request.CompanyId &&
               s.ParentId.HasValue);

            query = query.WhereOr(request.filters, request.defaultFilters);

            var combinedQuery = queryGroup.Union(query);

            //if (!string.IsNullOrEmpty(request.fieldSort))
            //{
            //    var columnSort = request.fieldSort;
            //    var sortType = request.sort;
            //    if (!string.IsNullOrEmpty(columnSort) && !request.isAdd && !request.isEdit)
            //    {
            //        var sort = request.sort == ESortEnum.desc.ToString() ? " DESC" : " ASC";
            //        combinedQuery = combinedQuery.OrderBy(columnSort + sort);
            //    }
            //}

            var items = await combinedQuery
                .Include(s => s.Parent)
                .ThenInclude(s => s.Parent)
                .ThenInclude(s => s.Parent)
                .ThenInclude(s => s.Parent)
                .Select(s => _mapper.Map<TeamResponseItem>(s)).ToListAsync();


            var parents = new List<TeamResponseItem>();
            var parentsRoot = new List<TeamResponseItem>();
            parentsRoot.AddRange(items.Where(s => !s.ParentId.HasValue).ToList());
            var itemsHasGroup = items.Where(s => s.ParentId.HasValue).ToList();
            foreach (var item in itemsHasGroup)
            {
                GetRecursiveParents(item, ref parents, ref parentsRoot);
            }

            var result = new List<TeamResponseItem>();
            //result.AddRange(parents);
            result.AddRange(parentsRoot);
            response.recordsTotal = result.Count;
            if (!string.IsNullOrEmpty(request.fieldSort))
            {
                var columnSort = request.fieldSort;
                var sortType = request.sort;
                if (!string.IsNullOrEmpty(columnSort) && !request.isAdd && !request.isEdit)
                {
                    var sort = request.sort == ESortEnum.desc.ToString() ? " DESC" : " ASC";
                    result = result.OrderByList(columnSort + sort);
                }
            }
            
            if (!request.isGetAll)
                result.Skip(request.start).Take(request.length);

            response.data.Items = result;

            return response;
        }

        // Hàm đệ quy để lấy danh sách đệ quy cha
        static void GetRecursiveParents(TeamResponseItem category, ref List<TeamResponseItem> parents, ref List<TeamResponseItem> parentsRoot)
        {
            if (category.Parent != null)
            {
                if (category.Parent.Parent != null)
                {
                    if (!parents.Any(s => s.Id == category.Parent.Id) && !parentsRoot.Any(s => s.Id == category.Parent.Id))
                    {
                        parents.Add(category.Parent);
                    }
                    GetRecursiveParents(category.Parent, ref parents, ref parentsRoot);
                }
                else
                {
                    if (!parentsRoot.Any(s => s.Id == category.Parent.Id) && !parents.Any(s => s.Id == category.Parent.Id))
                    {
                        parentsRoot.Add(category.Parent);
                    }
                }
            }
            else
            {
                if (!parentsRoot.Any(s => s.Id == category.Id) && !parents.Any(s => s.Id == category.Parent.Id))
                {
                    parentsRoot.Add(category);
                }
            }
        }
    }
}
