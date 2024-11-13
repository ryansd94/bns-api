using AutoMapper;
using BNS.Resource;
using BNS.Utilities;
using BNS.Domain.Responses;
using BNS.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BNS.Domain.Queries;
using BNS.Data.Entities.JM_Entities;
using System.Collections.Generic;
using System;

namespace BNS.Service.Features
{
    public class GetUserAssignQuery : IRequestHandler<GetUserAssignRequest, ApiResult<UserResponse>>
    {
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetUserAssignQuery(
            IStringLocalizer<SharedResource> sharedLocalizer,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _sharedLocalizer = sharedLocalizer;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        private void GetRecursiveChilds(List<TeamResponseItem> childs, ref List<TeamResponseItem> items)
        {
            foreach (var item in childs)
            {
                if (item.Childs.Any())
                {
                    GetRecursiveChilds(item.Childs.ToList(), ref items);
                }
                if (!items.Any(s => s.Id.Equals(item.Id)))
                {
                    items.Add(item);
                }
            }
        }

        public async Task<ApiResult<UserResponse>> Handle(GetUserAssignRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<UserResponse>();
            response.data = new UserResponse();
            var accountIds = new List<Guid>();

            if (request.ProjectId.HasValue)
            {
                var projectId = request.ProjectId.Value;
                var teams = await _unitOfWork.Repository<JM_ProjectTeam>()
                    .Include(s => s.JM_Team)
                    .ThenInclude(s => s.Childs)
                    .ThenInclude(s => s.Childs)
                    .ThenInclude(s => s.Childs)
                    .Where(s => s.ProjectId == projectId && !s.IsDelete)
                    .Select(s => _mapper.Map<TeamResponseItem>(s.JM_Team))
                    .ToListAsync();

                var allTeam = new List<TeamResponseItem>();
                GetRecursiveChilds(teams, ref allTeam);

                var teamIds = allTeam.Select(s => s.Id).Distinct().ToList();
                accountIds = await _unitOfWork.Repository<JM_AccountCompany>()
                    .Include(s => s.Account)
                    .Where(s => s.TeamId.HasValue && teamIds.Contains(s.TeamId.Value))
                    .Select(s => s.Account.Id).ToListAsync();

                var projectMemberIds = await _unitOfWork.Repository<JM_ProjectMember>()
                    .Where(s => s.ProjectId == projectId && !s.IsDelete && !accountIds.Contains(s.AccountCompanyId))
                    .Select(s => s.AccountCompanyId)
                    .ToListAsync();

                accountIds.AddRange(projectMemberIds);
            }

            var query = _unitOfWork.Repository<JM_AccountCompany>()
                .Include(s => s.Account)
                .Where(s => !s.IsDelete && s.CompanyId == request.CompanyId && s.Status == Enums.EUserStatus.ACTIVE && accountIds.Contains(s.Account.Id))
                .OrderBy(d => d.CreatedDate)
                .Select(s => new UserResponseItem
                {
                    Email = s.Account != null ? s.Account.Email : string.Empty,
                    Status = s.Status,
                    FullName = s.Account != null ? s.Account.FullName : string.Empty,
                    Name = s.Account != null ? s.Account.FullName : string.Empty,
                    Id = s.Id,
                    CreatedDate = s.CreatedDate,
                    IsMainAccount = s.IsMainAccount,
                    TeamName = s.JM_Team != null ? s.JM_Team.Name : string.Empty,
                    TeamId = s.TeamId,
                    Image = s.Account != null ? s.Account.Image : string.Empty,
                    FirstName = s.Account.FirstName,
                    LastName = s.Account.LastName
                });

            if (!string.IsNullOrEmpty(request.fieldSort))
            {
                query = query.OrderBy(request.fieldSort, request.sort);
            }
            if (!string.IsNullOrEmpty(request.filters))
                query = query.WhereOr(request.filters);
            response.recordsTotal = await query.CountAsync();

            if (!request.isGetAll)
                query = query.Skip(request.start).Take(request.length);

            var rs = await query.ToListAsync();
            response.data.Items = rs;
            return response;
        }
    }
}
