using AutoMapper;
using BNS.Data.Entities.JM_Entities;
using BNS.Domain;
using BNS.Domain.Commands;
using BNS.Resource;
using BNS.Resource.LocalizationResources;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;

namespace BNS.Service.Features
{
    public class CreateTaskCommand : IRequestHandler<CreateTaskRequest, ApiResult<Guid>>
    {
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateTaskCommand(
            IStringLocalizer<SharedResource> sharedLocalizer,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _sharedLocalizer = sharedLocalizer;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ApiResult<Guid>> Handle(CreateTaskRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<Guid>();

            var taskType = await _unitOfWork.Repository<JM_TaskType>()
                .Where(s => s.Id == request.DefaultData.TaskTypeId)
                .Include(s => s.Template)
                .ThenInclude(s => s.TemplateDetails)
                .FirstOrDefaultAsync();
            if (taskType == null)
            {
                response.errorCode = EErrorCode.IsExistsData.ToString();
                response.title = string.Format(_sharedLocalizer[LocalizedBackendMessages.MSG_ObjectNotExists], _sharedLocalizer[LocalizedBackendMessages.Task.TaskTypeName]);
                return response;
            }

            var task = _mapper.Map<JM_Task>(request.DefaultData);
            task.Id = Guid.NewGuid();
            task.CompanyId = request.CompanyId;
            task.CreatedDate = DateTime.UtcNow;
            task.CreatedUserId = request.UserId;
            task.ReporterId = request.UserId;

            _unitOfWork.Repository<JM_Task>().Add(task);

            #region Assign user

            if (request.DefaultData.UsersAssign != null && request.DefaultData.UsersAssign.Count > 0)
            {
                if (request.DefaultData.UsersAssign.Count == 1)
                {
                    task.AssignUserId = request.DefaultData.UsersAssign[0];
                }
                else
                {
                    for (int i = 0; i < request.DefaultData.UsersAssign.Count; i++)
                    {
                        var taskUser = new JM_TaskUser
                        {
                            Id = Guid.NewGuid(),
                            TaskId = task.Id,
                            UserId = request.DefaultData.UsersAssign[i],
                            IsDelete = false,
                            CompanyId = request.CompanyId,
                            CreatedDate = DateTime.UtcNow,
                            CreatedUserId = request.UserId,
                        };
                        _unitOfWork.Repository<JM_TaskUser>().Add(taskUser);
                    }
                }
            }

            #endregion

            #region Dynamic task data

            var templateDetails = taskType.Template?.TemplateDetails.ToList();
            var dataDynamics = request.DynamicData;

            if (templateDetails != null && templateDetails.Count > 0)
            {
                foreach (var value in dataDynamics)
                {
                    var customColumn = templateDetails.Where(s => s.Id.Equals(value.Key)).FirstOrDefault();
                    if (customColumn != null)
                    {
                        var taskCustomColumns = new JM_TaskCustomColumnValue
                        {
                            TaskId = task.Id,
                            CustomColumnId = customColumn.CustomColumnId.Value,
                            TemplateDetailId = customColumn.Id,
                            CreatedDate = DateTime.UtcNow,
                            CreatedUserId = request.UserId,
                            IsDelete = false,
                            CompanyId = request.CompanyId,
                            Value = value.Value.ToString(),
                        };
                        _unitOfWork.Repository<JM_TaskCustomColumnValue>().Add(taskCustomColumns);
                    }
                }
            }

            #endregion
            //var project = await _context.JM_Projects.Where(s => s.Id == request.ProjectId).FirstOrDefaultAsync();
            //if (project == null)
            //{
            //    response.errorCode = EErrorCode.IsExistsData.ToString();
            //    response.title = string.Format(_sharedLocalizer[LocalizedBackendMessages.MSG_ObjectNotExists], _sharedLocalizer[LocalizedBackendMessages.Project.ProjectName]);
            //    return response;
            //}

            //await _context.JM_Tasks.AddAsync(data);
            await _unitOfWork.SaveChangesAsync();
            //response.data = data.Id;
            return response;
        }

    }
}

