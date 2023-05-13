using BNS.Domain;
using BNS.Resource;
using MediatR;
using Microsoft.Extensions.Localization;
using Nest;
using System;
using System.Threading;
using System.Threading.Tasks;
using BNS.Domain.Commands;
using BNS.Data.Entities.JM_Entities;
using Microsoft.EntityFrameworkCore;
using static BNS.Utilities.Enums;
using BNS.Resource.LocalizationResources;

namespace BNS.Service.Features
{
    public class UpdateCommentCommand : IRequestHandler<UpdateCommentRequest, ApiResult<Guid>>
    {
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateCommentCommand(
         IStringLocalizer<SharedResource> sharedLocalizer,
         IUnitOfWork unitOfWork)
        {
            _sharedLocalizer = sharedLocalizer;
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResult<Guid>> Handle(UpdateCommentRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<Guid>();
            var dataCheck = await _unitOfWork.Repository<JM_Comment>().FirstOrDefaultAsync(s => s.Id == request.Id && s.CompanyId == request.CompanyId && s.CreatedUserId == request.UserId);
            if (dataCheck == null)
            {
                response.errorCode = EErrorCode.NotExistsData.ToString();
                response.title = _sharedLocalizer[LocalizedBackendMessages.MSG_ExistsData];
                return response;
            }

            dataCheck.Value = request.Value;
            dataCheck.UpdatedDate = DateTime.UtcNow;

            response = await _unitOfWork.SaveChangesAsync();
            return response;
        }
    }
}
