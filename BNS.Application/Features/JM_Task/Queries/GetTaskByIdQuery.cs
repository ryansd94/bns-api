
using AutoMapper;
using BNS.Data.Entities.JM_Entities;
using BNS.Domain;
using BNS.Domain.Commands;
using BNS.Domain.Queries;
using BNS.Domain.Responses;
using BNS.Resource;
using BNS.Resource.LocalizationResources;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;

namespace BNS.Service.Features
{
    public class GetTaskByIdQuery : IRequestHandler<GetTaskByIdRequest, ApiResult<TaskByIdResponse>>
    {
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private IMediator _mediator;

        public GetTaskByIdQuery(
            IUnitOfWork unitOfWork,
            IStringLocalizer<SharedResource> sharedLocalizer,
            IMediator mediator,
            IMapper mapper)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _sharedLocalizer = sharedLocalizer;
        }
        public async Task<ApiResult<TaskByIdResponse>> Handle(GetTaskByIdRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<TaskByIdResponse>();
            var task = await _unitOfWork.Repository<JM_Task>()
                .Where(s => s.Id == request.Id && !s.IsDelete)
                .Include(s => s.TaskUsers)
                .Include(s => s.User)
                .Include(s => s.JM_TaskParent)
                .Include(s => s.CommentTasks).ThenInclude(s => s.Comment)
                .ThenInclude(s => s.User)
                .Include(s => s.TaskTags).ThenInclude(s => s.Tag).FirstOrDefaultAsync();
            if (task == null)
            {
                response.errorCode = EErrorCode.NotExistsData.ToString();
                response.title = _sharedLocalizer[LocalizedBackendMessages.MSG_NotExistsData];
                return response;
            }
            var files = await _unitOfWork.Repository<JM_AttachedFiles>()
                .Include(s => s.File)
                .Where(s => s.EntityId == task.Id)
                .Select(s => new FileUpload
                {
                    Id = s.Id,
                    File = new FileItem
                    {
                        Name = s.File.Name
                    },
                    Url = s.File.Url,
                    IsDelete = s.IsDelete,
                })
                .ToListAsync();
            var taskItem = _mapper.Map<TaskItem>(task);
            var dynamicData = await _unitOfWork.Repository<JM_TaskCustomColumnValue>().Where(s => s.TaskId == task.Id
                 && !s.IsDelete).Select(s => new
                 {
                     id = s.TemplateDetailId.ToString().ToLower(),
                     value = s.Value
                 }).ToDictionaryAsync(r => r.id, r => r.value);
            taskItem.DynamicData = dynamicData;
            taskItem.Files = files;
            var taskTypeRequest = new GetTaskTypeByIdRequest
            {
                Id = task.TaskTypeId,
                CompanyId = request.CompanyId,
                IsDelete = null
            };
            var taskType = await _mediator.Send(taskTypeRequest);
            var taskChilds = await _unitOfWork.Repository<JM_Task>().Where(s => s.ParentId != null && s.ParentId == task.Id
              && !s.IsDelete).Select(s => _mapper.Map<TaskItem>(s)).ToListAsync();
            var comments = task.CommentTasks.ToList().Select(s => new TaskCommentReponse
            {
                Value = s.Comment.Value,
                Id = s.Comment.Id,
                User = new User
                {
                    Id = s.Comment.User.Id,
                    FullName = s.Comment.User.FullName,
                    Image = s.Comment.User.Image
                },
                UpdatedTime = s.Comment.UpdatedDate.ToString()
            }).ToList();

            response.data.Task = taskItem;
            response.data.TaskType = taskType.data;
            response.data.Task.TaskParent = _mapper.Map<TaskItem>(task.JM_TaskParent);
            response.data.Task.TaskChilds = taskChilds;
            response.data.Comments = comments;
            return response;
        }

    }
}
