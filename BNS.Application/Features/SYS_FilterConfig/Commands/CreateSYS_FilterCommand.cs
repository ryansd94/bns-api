using BNS.Data.Entities.JM_Entities;
using BNS.Domain;
using BNS.Resource;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Threading;
using System.Threading.Tasks;
using BNS.Domain.Commands;

namespace BNS.Service.Features
{
    public class CreateSYS_FilterCommand : IRequestHandler<CreateSYS_FilterRequest, ApiResult<Guid>>
    {
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IUnitOfWork _unitOfWork;
        public CreateSYS_FilterCommand(
         IStringLocalizer<SharedResource> sharedLocalizer,
         IUnitOfWork unitOfWork)
        {
            _sharedLocalizer = sharedLocalizer;
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResult<Guid>> Handle(CreateSYS_FilterRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<Guid>();
            var data = new SYS_FilterConfig
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                View = request.Component,
                UserId = request.UserId,
                FilterData = request.FilterData,
                CreatedDate = DateTime.UtcNow,
                CreatedUserId = request.UserId,
                CompanyId = request.CompanyId,
                IsDelete = false
            };
            await _unitOfWork.Repository<SYS_FilterConfig>().AddAsync(data);
            response = await _unitOfWork.SaveChangesAsync();
            return response;
        }
    }
}
