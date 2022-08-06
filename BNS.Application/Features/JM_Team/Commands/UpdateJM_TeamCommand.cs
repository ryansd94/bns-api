using BNS.Data.Entities.JM_Entities;
using BNS.Domain;
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

namespace BNS.Service.Features
{
    public class UpdateJM_TeamCommand : IRequestHandler<UpdateJM_TeamRequest, ApiResult<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public UpdateJM_TeamCommand(IUnitOfWork unitOfWork,
         IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _unitOfWork = unitOfWork;
            _sharedLocalizer = sharedLocalizer;
        }
        public async Task<ApiResult<Guid>> Handle(UpdateJM_TeamRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<Guid>();
            var dataCheck = await _unitOfWork.JM_TeamRepository.FirstOrDefaultAsync(s => s.Id == request.Id &&
            s.CompanyId == request.CompanyId);
            if (dataCheck == null)
            {
                response.errorCode = EErrorCode.NotExistsData.ToString();
                response.title = _sharedLocalizer[LocalizedBackendMessages.MSG_NotExistsData];
                return response;
            }

            var checkDuplicate = await _unitOfWork.JM_TeamRepository.FirstOrDefaultAsync(s => s.Name.Equals(request.Name)
            && s.Id != request.Id
            && s.CompanyId == request.CompanyId);
            if (checkDuplicate != null)
            {
                response.errorCode = EErrorCode.IsExistsData.ToString();
                response.title = _sharedLocalizer[LocalizedBackendMessages.MSG_ExistsData];
                return response;
            }
            dataCheck.Code = request.Code;
            dataCheck.Name = request.Name;
            dataCheck.Description = request.Description;
            dataCheck.ParentId = request.ParentId;
            dataCheck.UpdatedDate = DateTime.UtcNow;
            dataCheck.UpdatedUser = request.UserId;

            await _unitOfWork.JM_TeamRepository.UpdateAsync(dataCheck);
            response =await _unitOfWork.SaveChangesAsync();
            response.data = dataCheck.Id;
            return response;
        }

    }
}
