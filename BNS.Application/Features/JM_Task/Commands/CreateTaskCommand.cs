using AutoMapper;
using BNS.Data.Entities.JM_Entities;
using BNS.Domain;
using BNS.Domain.Commands;
using BNS.Domain.Interface;
using BNS.Domain.Messaging;
using BNS.Domain.Responses;
using BNS.Resource;
using BNS.Resource.LocalizationResources;
using BNS.Service.Subcriber;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using RawRabbit.Enrichers.MessageContext.Context;
using System;
using System.Collections.Generic;
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
        private readonly IAttachedFileService _attachedFileService;
        protected readonly INotifyService _notifyService;
        protected readonly ITaskService _taskService;
        private IMediator _mediator;
        private readonly IBusPublisher _busPublisher;

        public CreateTaskCommand(
            IStringLocalizer<SharedResource> sharedLocalizer,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IAttachedFileService attachedFileService,
            INotifyService notifyService,
            ITaskService taskService,
            IMediator mediator,
            IBusPublisher busPublisher)
        {
            _sharedLocalizer = sharedLocalizer;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _attachedFileService = attachedFileService;
            _notifyService = notifyService;
            _taskService = taskService;
            _mediator = mediator;
            _busPublisher = busPublisher;
        }

        private async Task SetTaskBumber(CreateTaskRequest request, JM_Task task)
        {
            await _busPublisher.PublishAsync(new SetTaskNumberSubcriber
            {
                CompanyId = request.CompanyId,
                Task = task
            });
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
            var userMentionIds = new List<string>();
            task.Id = Guid.NewGuid();
            task.CompanyId = request.CompanyId;
            task.CreatedDate = DateTime.UtcNow;
            task.CreatedUserId = request.UserId;
            task.ReporterId = request.UserId;
            _unitOfWork.Repository<JM_Task>().Add(task);

            #region Assign user

            if (request.DefaultData.UsersAssignId != null && request.DefaultData.UsersAssignId.Count > 0)
            {
                if (request.DefaultData.UsersAssignId.Count == 1)
                {
                    task.AssignUserId = request.DefaultData.UsersAssignId[0];
                }
                else
                {
                    for (int i = 0; i < request.DefaultData.UsersAssignId.Count; i++)
                    {
                        var taskUser = new JM_TaskUser
                        {
                            Id = Guid.NewGuid(),
                            TaskId = task.Id,
                            UserId = request.DefaultData.UsersAssignId[i],
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
                    var key = value.Key;
                    if (key.IndexOf('@') != -1)
                    {
                        key = key.Split('@')[0];
                    }
                    var customColumn = templateDetails.Where(s => s.Id.Equals(Guid.Parse(key))).FirstOrDefault();
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

            #region Tags
            if (request.DefaultData.Tags != null && request.DefaultData.Tags.Count > 0)
            {
                foreach (var tag in request.DefaultData.Tags)
                {
                    if (tag.IsDelete)
                        continue;
                    if (tag.IsAddNew)
                    {
                        _unitOfWork.Repository<JM_Tag>().Add(new JM_Tag
                        {
                            CompanyId = request.CompanyId,
                            CreatedDate = DateTime.UtcNow,
                            CreatedUserId = request.UserId,
                            Id = tag.Id.Value,
                            Name = tag.Name,
                            IsDelete = false,
                        });
                    }
                    _unitOfWork.Repository<JM_TaskTag>().Add(new JM_TaskTag
                    {
                        IsDelete = false,
                        Id = Guid.NewGuid(),
                        TagId = tag.Id.Value,
                        TaskId = task.Id,
                        CompanyId = request.CompanyId,
                        CreatedDate = DateTime.UtcNow,
                        CreatedUserId = request.UserId,
                    });
                }
            }
            #endregion

            #region Files
            if (request.DefaultData.Files != null && request.DefaultData.Files.Count > 0)
            {
                var fileAddNew = request.DefaultData.Files.Where(s => s.IsAddNew).Select(s => new CreateAttachedFilesRequest
                {
                    EntityId = task.Id,
                    CompanyId = request.CompanyId,
                    Url = s.Url,
                    UserId = request.UserId,
                    File = s.File,
                }).ToList();
                await _attachedFileService.AddAttachedFiles(fileAddNew);
            }
            #endregion

            #region Comments
            if (request.Comments != null && request.Comments.Count > 0)
            {
                for (int i = request.Comments.Count - 1; i >= 0; i--)
                {
                    await InsertComment(request.Comments[i], null, request.CompanyId, request.UserId, task.Id, userMentionIds);
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
            var notifyUserAssigns = await GetNotifyUsers(request.DefaultData.UsersAssignId, request.UserId, task,
                taskType.Color, taskType.Icon, ENotifyObjectType.TaskAssigned);
            if (notifyUserAssigns.Count > 0)
            {
                await _notifyService.SendNotify(notifyUserAssigns, request.UserId, request.CompanyId);
            }
            if (userMentionIds.Count > 0)
            {
                var userAssignIds = notifyUserAssigns.Select(s => s.UserReceivedId.ToString()).ToList();
                var userNotifyIds = userMentionIds.Where(s => !userAssignIds.Contains(s)).Select(s => Guid.Parse(s)).ToList();
                var userMentions = await GetNotifyUsers(userNotifyIds, request.UserId, task, taskType.Color, taskType.Icon, ENotifyObjectType.TaskCommentMention);
                await _notifyService.SendNotify(userMentions, request.UserId, request.CompanyId);
            }
            await SetTaskBumber(request, task);
            //response.data = data.Id;
            return response;
        }

        private async Task<List<NotifyResponse>> GetNotifyUsers(List<Guid> userMentionIds, Guid userCreateId, JM_Task task, string taskTypeColor, string taskTypeIcon, ENotifyObjectType notifyType)
        {
            var notifyResponses = new List<NotifyResponse>();
            if (userMentionIds == null || userMentionIds.Count == 0)
                return notifyResponses;
            try
            {
                var userMentions = await _unitOfWork.Repository<JM_Account>().AsNoTracking().Where(s => userMentionIds.Contains(s.Id) && !s.Id.Equals(userCreateId)).ToListAsync();
                if (userMentions.Count > 0)
                {
                    var userCreated = await _unitOfWork.Repository<JM_Account>().AsNoTracking().Where(s => s.Id == userCreateId).FirstOrDefaultAsync();
                    foreach (var item in userMentions)
                    {
                        var notifyResponse = _taskService.GetNotifyTaskResponse(task.Id, item.Id, userCreated, task, taskTypeColor, taskTypeIcon, notifyType);
                        notifyResponses.Add(notifyResponse);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return notifyResponses;
        }

        private async Task InsertComment(TaskCommentRequest commentRequest, Guid? parentId, Guid CompanyId, Guid userId,
            Guid taskID, List<string> userMentions, int level = 0)
        {
            var comment = new JM_Comment
            {
                Value = commentRequest.Value,
                Id = Guid.NewGuid(),
                CompanyId = CompanyId,
                CreatedUserId = userId,
                UpdatedUserId = userId,
                ParentId = parentId,
                Level = level,
                CountReply = commentRequest.Childrens != null ? commentRequest.Childrens.Count : 0,
            };
            _unitOfWork.Repository<JM_Comment>().Add(comment);
            _unitOfWork.Repository<JM_CommentTask>().Add(new JM_CommentTask
            {
                TaskId = taskID,
                CommentId = comment.Id,
                CreatedUserId = userId,
                CompanyId = CompanyId,
                UpdatedUserId = userId,
            });
            if (commentRequest.Value.Contains("data-id"))
            {
                var result = _taskService.GetUserMentionIdsWhenAddComments(commentRequest.Value);
                userMentions.AddRange(result);
            }
            if (commentRequest.Childrens != null)
            {
                foreach (var childComment in commentRequest.Childrens)
                {
                    await InsertComment(childComment, comment.Id, CompanyId, userId, taskID, userMentions, level + 1);
                }
            }
        }
    }
}

