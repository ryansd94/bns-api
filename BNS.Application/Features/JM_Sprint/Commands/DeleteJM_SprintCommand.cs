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
    public class DeleteJM_SprintCommand : IRequestHandler<DeleteJM_SprintRequest, ApiResult<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public DeleteJM_SprintCommand(IUnitOfWork unitOfWork,
         IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _unitOfWork = unitOfWork;
            _sharedLocalizer = sharedLocalizer;
        }
        public async Task<ApiResult<Guid>> Handle(DeleteJM_SprintRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<Guid>();
            var dataChecks = await _unitOfWork.Repository<JM_ProjectPhase>().Where(s => request.ids.Contains(s.Id)).ToListAsync();
            if (dataChecks == null || dataChecks.Count() == 0)
            {
                response.errorCode = EErrorCode.NotExistsData.ToString();
                response.title = _sharedLocalizer[LocalizedBackendMessages.MSG_NotExistsData];
                return response;
            }
            foreach (var item in dataChecks)
            {
                item.IsDelete = true;
                item.UpdatedDate = DateTime.UtcNow;
                item.UpdatedUserId = request.UserId;
                _unitOfWork.Repository<JM_ProjectPhase>().Update(item);
            }
            await _unitOfWork.SaveChangesAsync();
            return response;
        }

    }
}
