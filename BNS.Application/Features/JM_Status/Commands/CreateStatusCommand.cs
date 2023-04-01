using BNS.Data.Entities.JM_Entities;
using BNS.Domain;
using BNS.Resource;
using BNS.Resource.LocalizationResources;
using MediatR;
using Microsoft.Extensions.Localization;
using Nest;
using System;
using System.Threading;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;
using BNS.Domain.Commands;
using Microsoft.EntityFrameworkCore;

namespace BNS.Service.Features
{
    public class CreateStatusCommand : IRequestHandler<CreateStatusRequest, ApiResult<Guid>>
    {
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        protected readonly IElasticClient _elasticClient;
        private readonly IUnitOfWork _unitOfWork;
        public CreateStatusCommand(
         IStringLocalizer<SharedResource> sharedLocalizer,
         IElasticClient elasticClient,
         IUnitOfWork unitOfWork)
        {
            _sharedLocalizer = sharedLocalizer;
            _elasticClient = elasticClient;
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResult<Guid>> Handle(CreateStatusRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<Guid>();
            var dataCheck = await _unitOfWork.Repository<JM_Status>().FirstOrDefaultAsync(s => s.Name.Equals(request.Name) && s.CompanyId == request.CompanyId);
            if (dataCheck != null)
            {
                response.errorCode = EErrorCode.IsExistsData.ToString();
                response.title = _sharedLocalizer[LocalizedBackendMessages.MSG_ExistsData];
                return response;
            }
            var team = new JM_Status
            {
                Id = Guid.NewGuid(),
                Color = request.Color,
                Name = request.Name,
                CreatedDate = DateTime.UtcNow,
                CreatedUserId = request.UserId,
                CompanyId = request.CompanyId,
                IsStatusStart = request.IsStatusStart,
                IsStatusEnd = request.IsStatusEnd,
                IsDelete = false
            };
            await _unitOfWork.Repository<JM_Status>().AddAsync(team);
            response = await _unitOfWork.SaveChangesAsync();
            return response;
        }

    }
}
