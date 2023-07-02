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

namespace BNS.Service.Features
{
    public class CreateJM_SprintCommand : IRequestHandler<CreateJM_SprintRequest, ApiResult<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public CreateJM_SprintCommand(
         IStringLocalizer<SharedResource> sharedLocalizer,
         IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _sharedLocalizer = sharedLocalizer;
        }
        public async Task<ApiResult<Guid>> Handle(CreateJM_SprintRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<Guid>();
            var dataCheck = await _unitOfWork.Repository<JM_ProjectPhase>().FirstOrDefaultAsync(s => s.Name.Equals(request.Name)
            && s.CompanyId == request.CompanyId
            && s.ProjectId == request.JM_ProjectId);
            if (dataCheck != null)
            {
                response.errorCode = EErrorCode.IsExistsData.ToString();
                response.title = _sharedLocalizer[LocalizedBackendMessages.MSG_ExistsData];
                return response;
            }
            var data = new JM_ProjectPhase
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                ProjectId = request.JM_ProjectId,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                CreatedDate = DateTime.UtcNow,
                CreatedUserId = request.UserId,
            };
            await _unitOfWork.Repository<JM_ProjectPhase>().AddAsync(data);
            response = await _unitOfWork.SaveChangesAsync();
            return response;
        }

    }
}
