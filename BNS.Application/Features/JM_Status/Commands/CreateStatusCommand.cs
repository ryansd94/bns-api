using BNS.Data.Entities.JM_Entities;
using BNS.Domain;
using BNS.Resource.LocalizationResources;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;
using BNS.Domain.Commands;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace BNS.Service.Features
{
    public class CreateStatusCommand : IRequestHandler<CreateStatusRequest, ApiResult<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateStatusCommand(
         IMapper mapper,
         IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ApiResult<Guid>> Handle(CreateStatusRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<Guid>();
            var dataCheck = await _unitOfWork.Repository<JM_Status>().FirstOrDefaultAsync(s => s.Name.Equals(request.Name) && s.CompanyId == request.CompanyId);
            if (dataCheck != null)
            {
                response.errorCode = EErrorCode.IsExistsData.ToString();
                response.title = LocalizedBackendMessages.MSG_ExistsData;
                return response;
            }
            var data = _mapper.Map<JM_Status>(request);
            await _unitOfWork.Repository<JM_Status>().AddAsync(data);
            response = await _unitOfWork.SaveChangesAsync();
            return response;
        }

    }
}
