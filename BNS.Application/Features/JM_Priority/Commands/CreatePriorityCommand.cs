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
using AutoMapper;

namespace BNS.Service.Features
{
    public class CreatePriorityCommand : IRequestHandler<CreatePriorityRequest, ApiResult<Guid>>
    {
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreatePriorityCommand(
            IStringLocalizer<SharedResource> sharedLocalizer,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _sharedLocalizer = sharedLocalizer;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ApiResult<Guid>> Handle(CreatePriorityRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<Guid>();
            var dataCheck = await _unitOfWork.Repository<JM_Priority>().FirstOrDefaultAsync(s => s.Name.Equals(request.Name) && s.CompanyId == request.CompanyId);
            if (dataCheck != null)
            {
                response.errorCode = EErrorCode.IsExistsData.ToString();
                response.title = _sharedLocalizer[LocalizedBackendMessages.MSG_ExistsData];
                return response;
            }
            var data = _mapper.Map<JM_Priority>(request);
            await _unitOfWork.Repository<JM_Priority>().AddAsync(data);
            response = await _unitOfWork.SaveChangesAsync();
            return response;
        }

    }
}
