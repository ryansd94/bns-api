
using AutoMapper;
using BNS.Data.Entities.JM_Entities;
using BNS.Domain;
using BNS.Domain.Commands;
using BNS.Domain.Responses;
using BNS.Resource;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BNS.Service.Features
{
    public class GetTaskQuery : GetRequestHandler<TaskItem, JM_Task>
    {
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetTaskQuery(
            IStringLocalizer<SharedResource> sharedLocalizer,
            IMapper mapper,
            IUnitOfWork unitOfWork) : base(unitOfWork, mapper)
        {
            _sharedLocalizer = sharedLocalizer;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public override IQueryable<TaskItem> GetItemData(IQueryable<JM_Task> query)
        {
            var queryData = query.Select(s => new TaskItem
            {
                Id = s.Id,
                Title = s.Title,
                StartDate = s.StartDate,
                DueDate = s.DueDate,
                TaskType = new TaskType
                {
                    Name = s.TaskType.Name,
                    Color = s.TaskType.Color,
                    Icon = s.TaskType.Icon,
                },
                TaskTypeId = s.TaskTypeId,
                Status = new StatusResponseItem
                {
                    Name = s.Status != null ? s.Status.Name : "",
                    Color = s.Status != null ? s.Status.Color : "",
                },
                UsersAssign = s.AssignUser != null ? new List<User> {
                     _mapper.Map<User>(s.AssignUser)
                    } : (s.TaskUsers != null ? s.TaskUsers.Select(d => _mapper.Map<User>(d.User)).ToList() : null),
                StatusId = s.StatusId,
                Estimatedhour = s.Estimatedhour,
                CreatedDate = s.CreatedDate,
                CreatedUserId = s.CreatedUserId,
                Description = s.Description,
                ParentId = s.ParentId,
                ProjectId = s.ProjectId,
                CreatedUser = new User
                {
                    FullName = s.User != null ? s.User.FullName : "",
                    Image = s.User != null ? s.User.Image : "",
                },
                Tags = s.TaskTags != null ? s.TaskTags.Where(s => !s.IsDelete).Select(d => new TagItem { Id = d.Tag.Id, Name = d.Tag.Name }).ToList() : null,
                TaskCustomColumnValues = s.TaskCustomColumnValues != null ?
                s.TaskCustomColumnValues.Select(s => new TaskCustomColumnValue { Value = s.Value, CustomColumnId = s.CustomColumnId }).ToArray() : null,
            });
            return queryData;
        }

        public override IQueryable<JM_Task> GetQueryableData(CommandGetRequest<ApiResultList<TaskItem>> request)
        {
            var query = _unitOfWork.Repository<JM_Task>()
                .Include(s => s.TaskUsers)
                .Where(s => !s.IsDelete && s.CompanyId == request.CompanyId && (request.isMainAccount || s.ReporterId == request.UserId || (s.TaskUsers != null && s.TaskUsers.Any(d => d.UserId == request.UserId)) || s.CreatedUserId == request.UserId))
                  .OrderByDescending(d => d.CreatedDate).AsQueryable();

            return query;

        }
    }
}
