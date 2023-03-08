
using AutoMapper;
using BNS.Data.Entities.JM_Entities;
using BNS.Domain;
using BNS.Domain.Commands;
using BNS.Domain.Responses;
using BNS.Resource;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
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
                StatusId= s.StatusId,
                Estimatedhour = s.Estimatedhour,
                CreatedDate = s.CreatedDate,
                CreatedUserId = s.CreatedUserId,
                ParentId = s.ParentId,
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

        public override async Task<IQueryable<JM_Task>> GetQueryableData(CommandGetRequest<ApiResultList<TaskItem>> request)
        {
            var query1 = _unitOfWork.Repository<JM_Task>().Where(s => !s.IsDelete &&
              s.CompanyId == request.CompanyId && s.ReporterId == request.UserId)
                .OrderBy(d => d.CreatedDate).Select(s => s.Id);

            var query2 = _unitOfWork.Repository<JM_TaskUser>().Where(s => !s.IsDelete &&
               s.CompanyId == request.CompanyId && s.UserId == request.UserId)
                .OrderBy(d => d.CreatedDate).Select(s => s.TaskId);

            var query3 = await query1.Union(query2).Distinct().ToListAsync();

            var query = _unitOfWork.Repository<JM_Task>()
                .Where(s => query3.Contains(s.Id))
                .OrderByDescending(d => d.CreatedDate)
                .AsQueryable();
            return query;

        }
    }
}
