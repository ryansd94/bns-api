using BNS.Data.EntityContext;
using BNS.Resource;
using BNS.Resource.LocalizationResources;
using BNS.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;
using BNS.Domain.Commands;

namespace BNS.Service.Features
{
    public class UpdateJM_IssueCommand : IRequestHandler<UpdateJM_IssueRequest, ApiResult<Guid>>
    {
        protected readonly BNSDbContext _context;
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public UpdateJM_IssueCommand(BNSDbContext context,
         IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _context = context;
            _sharedLocalizer = sharedLocalizer;
        }
        public async Task<ApiResult<Guid>> Handle(UpdateJM_IssueRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<Guid>();
            var dataCheck = await _context.JM_Issues.Where(s => s.Id == request.Id).FirstOrDefaultAsync();
            if (dataCheck == null)
            {
                response.errorCode = EErrorCode.NotExistsData.ToString();
                response.title = _sharedLocalizer[LocalizedBackendMessages.MSG_NotExistsData];
                return response;
            }

            if (request.UserId != dataCheck.ReporterId)
            {
                if (dataCheck.IssueType != request.IssueType)
                {
                    response.errorCode = EErrorCode.Failed.ToString();
                    response.title = _sharedLocalizer[LocalizedBackendMessages.Message.MSG_NotPermissionEdit];
                    return response;
                }
            }

            dataCheck.SprintId = request.SprintId;
            dataCheck.StartDate = request.StartDate;
            dataCheck.DueDate = request.DueDate;
            dataCheck.IssueType = request.IssueType;
            dataCheck.IssueStatus = request.IssueStatus;
            dataCheck.Description = request.Description;
            dataCheck.IssueParentId = request.IssueParentId;
            dataCheck.OriginalTime = request.OriginalTime;
            dataCheck.RemainingTime = request.RemainingTime;
            dataCheck.AssignUserId = request.AssignUserId;
            dataCheck.Summary = request.Summary;
            dataCheck.UpdatedDate = DateTime.UtcNow;
            dataCheck.UpdatedUser = request.UserId;

            _context.JM_Issues.Update(dataCheck);
            await _context.SaveChangesAsync();
            response.data = dataCheck.Id;
            return response;
        }

    }

}
