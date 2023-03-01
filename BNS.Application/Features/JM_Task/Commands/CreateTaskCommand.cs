using AutoMapper;
using BNS.Data.Entities.JM_Entities;
using BNS.Domain;
using BNS.Domain.Commands;
using BNS.Domain.Interface;
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
        private readonly IAttachedFileService _attachedFileService;

        public CreateTaskCommand(
            IStringLocalizer<SharedResource> sharedLocalizer,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IAttachedFileService attachedFileService)
        {
            _sharedLocalizer = sharedLocalizer;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _attachedFileService = attachedFileService;
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
                    InsertComment(request.Comments[i], null, request.CompanyId, request.UserId, task.Id);
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

        private void InsertComment(TaskCommentRequest commentRequest, Guid? parentId, Guid CompanyId, Guid userId, Guid taskID, int level = 0)
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
            if (commentRequest.Childrens != null)
            {
                foreach (var childComment in commentRequest.Childrens)
                {
                    InsertComment(childComment, comment.Id, CompanyId, userId, taskID, level + 1);
                }
            }
        }
    }
}

