using BNS.Data.Entities.JM_Entities;
using BNS.Domain;
using BNS.Domain.Commands;
using BNS.Resource;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BNS.Service.Features
{
    public class RealAllNotifyCommand : IRequestHandler<ReadAllNotifyRequest, ApiResult<Guid>>
    {
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IUnitOfWork _unitOfWork;
        public RealAllNotifyCommand(
         IStringLocalizer<SharedResource> sharedLocalizer,
         IUnitOfWork unitOfWork)
        {
            _sharedLocalizer = sharedLocalizer;
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResult<Guid>> Handle(ReadAllNotifyRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<Guid>();
            var notifies = await _unitOfWork.Repository<JM_NotifycationUser>().Where(s => s.UserReceivedId == request.UserId &&
            s.CompanyId == request.CompanyId &&
            s.IsRead == false &&
            s.IsDelete == false).ToListAsync();

            foreach (var item in notifies)
            {
                item.IsRead = true;
            }
            _unitOfWork.Repository<JM_NotifycationUser>().UpdateRange(notifies);
            response = await _unitOfWork.SaveChangesAsync();
            return response;
        }
    }
}
