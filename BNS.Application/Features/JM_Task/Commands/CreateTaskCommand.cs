using BNS.Data.Entities.JM_Entities;
using BNS.Data.EntityContext;
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
using BNS.Domain.Commands;
using BNS.Domain;
using System.Collections.Generic;

namespace BNS.Service.Features
{
    public class CreateTaskCommand : IRequestHandler<CreateTaskRequest, ApiResult<Guid>>
    {
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IUnitOfWork _unitOfWork;

        public CreateTaskCommand(
         IStringLocalizer<SharedResource> sharedLocalizer,
         IUnitOfWork unitOfWork)
        {
            _sharedLocalizer = sharedLocalizer;
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResult<Guid>> Handle(CreateTaskRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<Guid>();

            var taskType = await _unitOfWork.Repository<JM_TaskType>()
                .Where(s => s.Id == request.TaskTypeId)
                .Include(s => s.Template)
                .ThenInclude(s => s.TemplateDetails)
                .FirstOrDefaultAsync();
            if (taskType == null)
            {
                response.errorCode = EErrorCode.IsExistsData.ToString();
                response.title = string.Format(_sharedLocalizer[LocalizedBackendMessages.MSG_ObjectNotExists], _sharedLocalizer[LocalizedBackendMessages.Task.TaskTypeName]);
                return response;
            }

            var task = new JM_Task
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                IsDelete = false,
                CompanyId = request.CompanyId,
                CreatedDate = DateTime.UtcNow,
                CreatedUserId = request.UserId,
                ReporterId = request.UserId,
                TaskTypeId = taskType.Id,
                StatusId = request.StatusId
            };
            _unitOfWork.Repository<JM_Task>().Add(task);

            #region Dynamic task data

            #region Assign user

            if (request.UsersAssign != null && request.UsersAssign.Count > 0)
            {
                if (request.UsersAssign.Count == 1)
                {
                    task.AssignUserId = request.UsersAssign[0];
                }
                else
                {
                    for (int i = 0; i < request.UsersAssign.Count; i++)
                    {
                        var taskUser = new JM_TaskUser
                        {
                            Id = Guid.NewGuid(),
                            TaskId = task.Id,
                            UserId = request.UsersAssign[i],
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

            var templateDetails = taskType.Template?.TemplateDetails.ToList();
            var dataDynamics = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(Convert.ToString(request.Data));

            if (templateDetails != null && templateDetails.Count > 0)
            {
                foreach (var value in dataDynamics)
                {
                    var customColumn = templateDetails.Where(s => s.Id.ToString().Equals(value.Key)).FirstOrDefault();
                    if (customColumn != null)
                    {
                        var taskCustomColumns = new JM_TaskCustomColumn
                        {
                            TaskId = task.Id,
                            CustomColumnId = customColumn.CustomColumnId.Value,
                            CreatedDate = DateTime.UtcNow,
                            CreatedUserId = request.UserId,
                            IsDelete = false,
                            CompanyId = request.CompanyId,
                            Value = value.Value.ToString(),
                        };
                        _unitOfWork.Repository<JM_TaskCustomColumn>().Add(taskCustomColumns);
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

