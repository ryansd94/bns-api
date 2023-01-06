
using AutoMapper;
using BNS.Data.Entities.JM_Entities;
using BNS.Domain;
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
                .Include(s => s.TaskTags).ThenInclude(s => s.Tag)
                .Select(s => _mapper.Map<TaskItem>(s)).FirstOrDefaultAsync();
            if (task == null)
            {
                response.errorCode = EErrorCode.NotExistsData.ToString();
                response.title = _sharedLocalizer[LocalizedBackendMessages.MSG_NotExistsData];
                return response;
            }
            var dynamicData = await _unitOfWork.Repository<JM_TaskCustomColumnValue>().Where(s => s.TaskId == task.Id
                 && !s.IsDelete).Select(s => new
                 {
                     id = s.TemplateDetailId.ToString().ToLower(),
                     value = s.Value
                 }).ToDictionaryAsync(r => r.id, r => r.value);
            task.DynamicData = dynamicData;
            var taskTypeRequest = new GetTaskTypeByIdRequest
            {
                Id = task.TaskTypeId,
                CompanyId = request.CompanyId,
                IsDelete = null
            };
            var taskType = await _mediator.Send(taskTypeRequest);
            response.data.Task = task;
            response.data.TaskType = taskType.data;
            return response;
        }

    }
}
