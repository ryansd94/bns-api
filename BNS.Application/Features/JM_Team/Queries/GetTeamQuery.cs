
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

        public override async Task<ApiResultList<TeamResponseItem>> Handle(GetTeamRequest request, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(request.filters))
            {
                return await GetDataByFilter(request);
            }
            else
            {
                return await GetData(request);
            }
        }

        private async Task<ApiResultList<TeamResponseItem>> GetDataByFilter(GetTeamRequest request)
        {
            var response = new ApiResultList<TeamResponseItem>();
            response.data = new DynamicDataItem<TeamResponseItem>();
            var queryGroup = _unitOfWork.Repository<JM_Team>()
                .Include(s => s.Parent)
               .Where(s => !s.IsDelete && s.CompanyId == request.CompanyId &&
               !s.ParentId.HasValue);

            queryGroup = queryGroup.WhereOr<JM_Team>(request.filters, request.defaultFilters);


            var query = _unitOfWork.Repository<JM_Team>()
                //.Include(s => s.Parent)
               .Where(s => !s.IsDelete && s.CompanyId == request.CompanyId &&
               s.ParentId.HasValue);

            query = query.WhereOr<JM_Team>(request.filters, request.defaultFilters);


            if (!string.IsNullOrEmpty(request.fieldSort))
            {
                var columnSort = request.fieldSort;
                var sortType = request.sort;
                if (!string.IsNullOrEmpty(columnSort) && !request.isAdd && !request.isEdit)
                {
                    var sort = request.sort == ESortEnum.desc.ToString() ? " DESC" : " ASC";
                    queryGroup = queryGroup.OrderBy(columnSort + sort);
                    query = query.OrderBy(columnSort + sort);
                }
            }
            var groupIds = queryGroup.Select(s => s.Id).Distinct();

            var itemsNotHasGroup = await queryGroup.ToListAsync();
            var itemsHasGroup = await query.ToListAsync();

            var uuu = await query.Select(s => _mapper.Map<TeamResponseItem>(s)).ToListAsync();

            var parents = new List<JM_Team>();
            var parentsRoot = new List<JM_Team>();
            parentsRoot.AddRange(itemsNotHasGroup);
            foreach (var item in itemsHasGroup)
            {
                GetRecursiveParents(item, ref parents, ref parentsRoot);
            }

            var result = new List<JM_Team>();
            //result.AddRange(parents);
            result.AddRange(parentsRoot);
            response.recordsTotal = result.Count;
            if (!request.isGetAll)
                result.Skip(request.start).Take(request.length);

            response.data.Items = result.Select(s => _mapper.Map<TeamResponseItem>(s)).ToList();

            return response;
        }

        // Hàm đệ quy để lấy danh sách đệ quy cha
        static void GetRecursiveParents(JM_Team category, ref List<JM_Team> parents, ref List<JM_Team> parentsRoot)
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

        public async Task<ApiResultList<TeamResponseItem>> GetData(GetTeamRequest request)
        {
            var response = new ApiResultList<TeamResponseItem>();
            response.data = new DynamicDataItem<TeamResponseItem>();
            var query = _unitOfWork.Repository<JM_Team>()
                .Include(s => s.Parent)
               .Include(s => s.Childs)
               .ThenInclude(s => s.Childs)
               .ThenInclude(s => s.Childs)
               .ThenInclude(s => s.Childs)
               .OrderBy(d => d.CreatedDate)
               .Where(s => !s.IsDelete && s.CompanyId == request.CompanyId &&
               (request.IsParentChild == false || (request.IsParentChild != false && !s.ParentId.HasValue)))
               .Select(s => _mapper.Map<TeamResponseItem>(s));

            response.recordsTotal = await query.CountAsync();
            if (!request.isGetAll)
                query = query.Skip(request.start).Take(request.length);

            response.data.Items = await query.ToListAsync();

            return response;
        }
    }
}
