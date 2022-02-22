using BNS.Application.Interface;
using BNS.Data.EntityContext;
using BNS.Resource;
using BNS.Resource.LocalizationResources;
using BNS.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;
namespace BNS.Application.Features
{
    public class DeleteJM_TeamCommand
    {
        public class DeleteJM_TeamRequest : CommandBase<ApiResult<Guid>>
        {
            [Required]
            public List<Guid> ids { get; set; } = new List<Guid>();
        }
        public class DeleteJM_TeamCommandHandler : IRequestHandler<DeleteJM_TeamRequest, ApiResult<Guid>>
        {
            private readonly IUnitOfWork _unitOfWork;
            protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;

            public DeleteJM_TeamCommandHandler(IUnitOfWork unitOfWork,
             IStringLocalizer<SharedResource> sharedLocalizer)
            {
                _unitOfWork = unitOfWork;
                _sharedLocalizer = sharedLocalizer;
            }
            public async Task<ApiResult<Guid>> Handle(DeleteJM_TeamRequest request, CancellationToken cancellationToken)
            {
                var response = new ApiResult<Guid>();
                var dataChecks = await _unitOfWork.JM_TeamRepository.GetAsync(s => request.ids.Contains(s.Id));
                if (dataChecks == null || dataChecks.Count() ==0)
                {
                    response.errorCode = EErrorCode.NotExistsData.ToString();
                    response.title = _sharedLocalizer[LocalizedBackendMessages.MSG_NotExistsData];
                    return response;
                }
                foreach (var item in dataChecks)
                {
                    item.IsDelete = true;
                    item.UpdatedDate = DateTime.UtcNow;
                    item.UpdatedUser = request.CreatedBy;
                    await _unitOfWork.JM_TeamRepository.UpdateAsync(item);
                }
                await _unitOfWork.SaveChangesAsync();
                return response;
            }

        }
    }
}
