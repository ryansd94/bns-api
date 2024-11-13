
using AutoMapper;
using BNS.Data.Entities.JM_Entities;
using BNS.Domain;
using BNS.Domain.Queries;
using BNS.Domain.Responses;
using BNS.Resource;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static BNS.Resource.LocalizationResources.LocalizedBackendMessages;

namespace BNS.Service.Features
{
    public class GetTaskCalendarQuery : GetRequestHandler<TaskCalendarRespone, JM_Task, GetTaskCalendarRequest>
    {
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetTaskCalendarQuery(
            IStringLocalizer<SharedResource> sharedLocalizer,
            IMapper mapper,
            IUnitOfWork unitOfWork) : base(unitOfWork, mapper)
        {
            _sharedLocalizer = sharedLocalizer;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public override async Task<ApiResultList<TaskCalendarRespone>> Handle(GetTaskCalendarRequest request, CancellationToken cancellationToken)
        {
            var result = new ApiResultList<TaskCalendarRespone>();
            var resulData = new List<TaskCalendarRespone>();
            var resources = new List<TaskCalendarResource>();
            var project = await _unitOfWork.Repository<JM_Project>()
                .Include(s => s.JM_ProjectTeams)
                .Include(s => s.JM_ProjectMembers).ThenInclude(s => s.AccountCompany)
                .FirstOrDefaultAsync(s => s.Id == request.ProjectId);
            if (project == null)
            {
                return result;
            }
            var tasks = await _unitOfWork.Repository<JM_Task>()
                .Include(s => s.TaskType)
                .Include(s => s.Status)
                .Include(s => s.Childs)
                .ThenInclude(s => s.Childs)
                .Include(s => s.TaskTags)
                .Include(s => s.TaskCustomColumnValues)
                .Include(s => s.AssignUser)
                .ThenInclude(s => s.Account)
                .Where(s => !s.IsDelete && s.CompanyId == request.CompanyId &&
                s.AssignUserId != null &&
                !s.ParentId.HasValue &&
                (!request.ProjectId.HasValue || (request.ProjectId.HasValue && s.ProjectId == request.ProjectId.Value)) &&
                (request.isMainAccount || s.ReporterId == request.UserId || s.CreatedUserId == request.UserId))
            .OrderByDescending(d => d.CreatedDate)
                  .Select(s => _mapper.Map<TaskCalendarEventItem>(s))
                  .ToListAsync();

            var teamIds = project.JM_ProjectTeams.Select(s => s.TeamId).ToList();
            var projectMemberTeamIds = project.JM_ProjectMembers.Where(s => s.AccountCompany.TeamId.HasValue).Select(s => s.AccountCompany.TeamId.Value).Distinct().ToList();
            teamIds.AddRange(projectMemberTeamIds);
            teamIds = teamIds.Distinct().ToList();

            if (teamIds.Count > 0)
            {
                resources = await _unitOfWork.Repository<JM_Team>()
                   .OrderBy(d => d.CreatedDate)
                   .Where(s => !s.IsDelete && s.CompanyId == request.CompanyId && !s.ParentId.HasValue && teamIds.Contains(s.Id))
                   .Select(s => new TaskCalendarResource
                   {
                       Id = s.Id,
                       Name = s.Name,
                       ParentId = s.ParentId
                   })
                   .ToListAsync();
            }
            resulData.Add(new TaskCalendarRespone
            {
                Resources = resources,
                Events = tasks
            });
            result.data.Items = resulData;

            return result;
        }
    }
}
