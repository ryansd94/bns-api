using BNS.Application.Interface;
using BNS.Data.Entities.JM_Entities;
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
    public class CreateJM_SprintCommand
    {
        public class CreateJM_SprintRequest : CommandBase<ApiResult<Guid>>
        {
            [Required]
            public string Name { get; set; }
            public string Description { get; set; }
            [Required]
            public DateTime StartDate { get; set; }
            [Required]
            public DateTime EndDate { get; set; }
            [Required]
            public Guid JM_ProjectId { get; set; }
        }
        public class CreateJM_SprintCommandHandler : IRequestHandler<CreateJM_SprintRequest, ApiResult<Guid>>
        {
            private readonly IUnitOfWork _unitOfWork;
            protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;

            public CreateJM_SprintCommandHandler(
             IStringLocalizer<SharedResource> sharedLocalizer,
             IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
                _sharedLocalizer = sharedLocalizer;
            }
            public async Task<ApiResult<Guid>> Handle(CreateJM_SprintRequest request, CancellationToken cancellationToken)
            {
                var response = new ApiResult<Guid>();
                var dataCheck = await _unitOfWork.JM_SprintRepository.GetDefaultAsync(s => s.Name.Equals(request.Name)
                && s.CompanyIndex == request.CompanyId
                && s.JM_ProjectId == request.JM_ProjectId);
                if (dataCheck != null)
                {
                    response.errorCode = EErrorCode.IsExistsData.ToString();
                    response.title = _sharedLocalizer[LocalizedBackendMessages.MSG_ExistsData];
                    return response;
                }
                var data = new JM_Sprint
                {
                    Id = Guid.NewGuid(),
                    Name = request.Name,
                    Description = request.Description,
                    JM_ProjectId = request.JM_ProjectId,
                    StartDate = request.StartDate,
                    EndDate = request.EndDate,
                    CreatedDate = DateTime.UtcNow,
                    CreatedUser = request.UserId,
                };
                await _unitOfWork.JM_SprintRepository.AddAsync(data);
                response = await _unitOfWork.SaveChangesAsync();
                return response;
            }

        }
    }
}
