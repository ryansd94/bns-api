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
    public class ChangeTaskTypeCommand : IRequestHandler<ChangeTaskTypeRequest, ApiResult<Guid>>
    {
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IUnitOfWork _unitOfWork;

        public ChangeTaskTypeCommand(
            IUnitOfWork unitOfWork,
            IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _sharedLocalizer = sharedLocalizer;
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResult<Guid>> Handle(ChangeTaskTypeRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<Guid>();
            var dataCheck = await _unitOfWork.Repository<JM_Task>()
                .Where(s => s.Id == request.Id).FirstOrDefaultAsync();
            if (dataCheck == null)
            {
                response.errorCode = EErrorCode.NotExistsData.ToString();
                response.title = _sharedLocalizer[LocalizedBackendMessages.MSG_NotExistsData];
                return response;
            }

            dataCheck.TaskTypeId = request.TaskTypeId;
            dataCheck.UpdatedDate = DateTime.UtcNow;
            dataCheck.UpdatedUserId = request.UserId;

            _unitOfWork.Repository<JM_Task>().Update(dataCheck);
            await _unitOfWork.SaveChangesAsync();
            response.data = dataCheck.Id;
            return response;
        }

    }
}
