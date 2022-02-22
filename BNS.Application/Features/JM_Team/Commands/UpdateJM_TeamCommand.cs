using BNS.Application.Interface;
using BNS.Resource;
using BNS.Resource.LocalizationResources;
using BNS.ViewModels;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;

namespace BNS.Application.Features
{
    public class UpdateJM_TeamCommand
    {
        public class UpdateJM_TeamRequest : CommandBase<ApiResult<Guid>>
        {
            [Required]
            public string Name { get; set; }
            [Required]
            public Guid Id { get; set; }
            public string Code { get; set; }
            public string Description { get; set; }
            public Guid? ParentId { get; set; }
        }
        public class UpdateJM_TeamCommandHandler : IRequestHandler<UpdateJM_TeamRequest, ApiResult<Guid>>
        {
            private readonly IUnitOfWork _unitOfWork;
            protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;

            public UpdateJM_TeamCommandHandler(IUnitOfWork unitOfWork,
             IStringLocalizer<SharedResource> sharedLocalizer)
            {
                _unitOfWork = unitOfWork;
                _sharedLocalizer = sharedLocalizer;
            }
            public async Task<ApiResult<Guid>> Handle(UpdateJM_TeamRequest request, CancellationToken cancellationToken)
            {
                var response = new ApiResult<Guid>();
                var dataCheck = await _unitOfWork.JM_TeamRepository.GetDefaultAsync(s => s.Id == request.Id);
                if (dataCheck == null)
                {
                    response.errorCode = EErrorCode.NotExistsData.ToString();
                    response.title = _sharedLocalizer[LocalizedBackendMessages.MSG_NotExistsData];
                    return response;
                }

                var checkDuplicate = await _unitOfWork.JM_TeamRepository.GetDefaultAsync(s => s.Name.Equals(request.Name)  && s.Id != request.Id);
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
                dataCheck.UpdatedUser = request.CreatedBy;

                await _unitOfWork.JM_TeamRepository.UpdateAsync(dataCheck);
                await _unitOfWork.SaveChangesAsync();
                response.data = dataCheck.Id;
                return response;
            }

        }
    }
}
