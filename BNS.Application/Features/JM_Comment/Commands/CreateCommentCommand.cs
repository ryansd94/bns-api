using BNS.Data.Entities.JM_Entities;
using BNS.Domain;
using BNS.Resource;
using BNS.Resource.LocalizationResources;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Threading;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;
using BNS.Domain.Commands;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using BNS.Domain.Interface;
using BNS.Domain.Responses;

namespace BNS.Service.Features
{
    public class CreateCommentCommand : IRequestHandler<CreateCommentRequest, ApiResult<Guid>>
    {
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        protected readonly INotifyService _notifyService;
        private readonly IUnitOfWork _unitOfWork;
        protected readonly ITaskService _taskService;

        public CreateCommentCommand(
            IStringLocalizer<SharedResource> sharedLocalizer,
            IUnitOfWork unitOfWork,
            INotifyService notifyService,
            ITaskService taskService)
        {
            _sharedLocalizer = sharedLocalizer;
            _unitOfWork = unitOfWork;
            _notifyService = notifyService;
            _taskService = taskService;
        }

        public async Task<ApiResult<Guid>> Handle(CreateCommentRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<Guid>();
            var dataTask = await _unitOfWork.Repository<JM_Task>().Include(s => s.TaskType).FirstOrDefaultAsync(s => s.Id == request.TaskId && s.CompanyId == request.CompanyId);
            if (dataTask == null)
            {
                response.errorCode = EErrorCode.NotExistsData.ToString();
                response.title = _sharedLocalizer[LocalizedBackendMessages.MSG_ExistsData];
                return response;
            }

            var lstDataNotify = new List<NotifyResponse>();
            var lstNotify = await InsertComment(new TaskCommentRequest
            {
                Id = request.Id,
                Value = request.Value
            }, request.ParentId, request.CompanyId, request.UserId, request.TaskId);

            #region send notify user if reply comment

            if (request.ParentId != null)
            {
                var parentComment = await _unitOfWork.Repository<JM_Comment>().FirstOrDefaultAsync(s => s.Id == request.ParentId.Value && !s.IsDelete);
                if (parentComment != null && parentComment.CreatedUserId != request.UserId)
                {
                    var userMenton = await _unitOfWork.Repository<JM_Account>().Where(s => s.Id == request.UserId).FirstOrDefaultAsync();
                    var notifyReply = _taskService.GetNotifyTaskResponse(request.Id, parentComment.CreatedUserId, userMenton, dataTask, ENotifyObjectType.TaskCommentReply);
                    lstDataNotify.Add(notifyReply);
                }
            }

            #endregion

            if (lstNotify.Count > 0)
            {
                var userReceivedIds = lstDataNotify.Select(s => s.UserReceivedId).ToList();
                lstDataNotify.AddRange(lstNotify.Where(s => !userReceivedIds.Contains(s.UserReceivedId)).ToList());
            }

            #region send notify to users mention in comment

            if (lstDataNotify.Count > 0)
            {
                await _notifyService.SendNotify(lstDataNotify, request.UserId, request.CompanyId);
            }

            #endregion

            response = await _unitOfWork.SaveChangesAsync();
            return response;
        }

        private async Task<List<NotifyResponse>> InsertComment(TaskCommentRequest commentRequest, Guid? parentId, Guid CompanyId, Guid userId, Guid taskID)
        {
            JM_Comment commentParent = null;
            var result = new List<NotifyResponse>();
            if (parentId != null)
            {
                commentParent = await _unitOfWork.Repository<JM_Comment>().Where(s => s.Id == parentId.Value).FirstOrDefaultAsync();
                if (commentParent != null)
                {
                    commentParent.CountReply += 1;
                }
                _unitOfWork.Repository<JM_Comment>().Update(commentParent);
            }
            var comment = new JM_Comment
            {
                Value = commentRequest.Value,
                Id = commentRequest.Id.Value,
                CompanyId = CompanyId,
                CreatedUserId = userId,
                UpdatedUserId = userId,
                ParentId = parentId,
                Level = commentParent == null ? 0 : commentParent.Level + 1,
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
                result = await _taskService.GetUserMentionNotifyWhenAddComments(commentRequest.Value, taskID, userId, comment.Id);
            }
            return result;
        }
    }
}
