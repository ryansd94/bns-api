using BNS.Data.Entities.JM_Entities;
using BNS.Domain;
using BNS.Resource;
using BNS.Resource.LocalizationResources;
using MediatR;
using Microsoft.Extensions.Localization;
using Nest;
using System;
using System.Threading;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;
using BNS.Domain.Commands;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BNS.Service.Features
{
    public class CreateCommentCommand : IRequestHandler<CreateCommentRequest, ApiResult<Guid>>
    {
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        protected readonly IElasticClient _elasticClient;
        private readonly IUnitOfWork _unitOfWork;
        public CreateCommentCommand(
         IStringLocalizer<SharedResource> sharedLocalizer,
         IElasticClient elasticClient,
         IUnitOfWork unitOfWork)
        {
            _sharedLocalizer = sharedLocalizer;
            _elasticClient = elasticClient;
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResult<Guid>> Handle(CreateCommentRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<Guid>();
            var dataCheck = await _unitOfWork.Repository<JM_Task>().FirstOrDefaultAsync(s => s.Id == request.TaskId && s.CompanyId == request.CompanyId);
            if (dataCheck == null)
            {
                response.errorCode = EErrorCode.NotExistsData.ToString();
                response.title = _sharedLocalizer[LocalizedBackendMessages.MSG_ExistsData];
                return response;
            }

            await InsertComment(new TaskCommentRequest
            {
                Id = request.Id,
                Value = request.Value
            }, request.ParentId, request.CompanyId, request.UserId, request.TaskId);

            response = await _unitOfWork.SaveChangesAsync();
            return response;
        }


        private async Task InsertComment(TaskCommentRequest commentRequest, Guid? parentId, Guid CompanyId, Guid userId, Guid taskID)
        {
            JM_Comment commentParent = null;
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
        }

    }
}
