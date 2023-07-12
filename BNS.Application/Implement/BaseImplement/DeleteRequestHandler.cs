using BNS.Data.Entities.JM_Entities;
using BNS.Domain;
using BNS.Resource.LocalizationResources;
using MediatR;
using Microsoft.EntityFrameworkCore;
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

        public DeleteRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResult<Guid>> Handle(TRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<Guid>();
            var dataChecks = await _unitOfWork.Repository<TEntity>().Where(s => request.Ids.Contains(s.Id) && s.CompanyId == request.CompanyId).ToListAsync();
            if (dataChecks.Count == 0)
            {
                response.errorCode = EErrorCode.NotExistsData.ToString();
                response.title = LocalizedBackendMessages.MSG_NotExistsData;
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
            return response;
        }
    }
}
