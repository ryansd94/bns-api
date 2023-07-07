using BNS.Data.Entities.JM_Entities;
using BNS.Domain;
using BNS.Resource;
using BNS.Resource.LocalizationResources;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;

namespace BNS.Service.Implement.BaseImplement
{
    public class DeleteRequestHandler<TRequest, TEntity> : IRequestHandler<TRequest, ApiResult<Guid>> where TEntity : BaseJMEntity where TRequest : CommandDeleteBase<ApiResult<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        public DeleteRequestHandler(IUnitOfWork unitOfWork,
            IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _unitOfWork = unitOfWork;
            _sharedLocalizer = sharedLocalizer;
        }

        public async Task<ApiResult<Guid>> Handle(TRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<Guid>();
            var dataChecks = await _unitOfWork.Repository<TEntity>().Where(s => request.ids.Contains(s.Id) && s.CompanyId == request.CompanyId).ToListAsync();
            if (dataChecks.Count == 0)
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
            }
            _unitOfWork.Repository<TEntity>().UpdateRange(dataChecks);
            response = await _unitOfWork.SaveChangesAsync();
            response.data = (Guid)dataChecks.GetType().GetProperty("Id").GetValue(dataChecks, null);
            return response;
        }
    }
}
