using BNS.Resource;
using BNS.Resource.LocalizationResources;
using BNS.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;
using BNS.Domain.Commands;
using BNS.Data.Entities.JM_Entities;

namespace BNS.Service.Features
{
    public class UpdateJM_SprintCommand : IRequestHandler<UpdateJM_SprintRequest, ApiResult<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public UpdateJM_SprintCommand(IUnitOfWork unitOfWork,
         IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _unitOfWork = unitOfWork;
            _sharedLocalizer = sharedLocalizer;
        }
        public async Task<ApiResult<Guid>> Handle(UpdateJM_SprintRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<Guid>();
            var dataCheck = await _unitOfWork.Repository<JM_ProjectPhase>().Where(s => s.Id == request.Id).FirstOrDefaultAsync();
            if (dataCheck == null)
            {
                response.errorCode = EErrorCode.NotExistsData.ToString();
                response.title = _sharedLocalizer[LocalizedBackendMessages.MSG_NotExistsData];
                return response;
            }

            var checkDuplicate = await _unitOfWork.Repository<JM_ProjectPhase>().Where(s => s.Name.Equals(request.Name) && s.Id != request.Id).FirstOrDefaultAsync();
            if (checkDuplicate != null)
            {
                response.errorCode = EErrorCode.IsExistsData.ToString();
                response.title = _sharedLocalizer[LocalizedBackendMessages.MSG_ExistsData];
                return response;
            }
            dataCheck.StartDate = request.StartDate;
            dataCheck.EndDate = request.EndDate;
            dataCheck.Name = request.Name;
            dataCheck.UpdatedDate = DateTime.UtcNow;
            dataCheck.UpdatedUserId = request.UserId;

            _unitOfWork.Repository<JM_ProjectPhase>().Update(dataCheck);
            await _unitOfWork.SaveChangesAsync();
            response.data = dataCheck.Id;
            return response;
        }

    }
}
