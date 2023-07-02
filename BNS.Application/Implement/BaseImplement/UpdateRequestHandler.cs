using AutoMapper;
using BNS.Data.Entities.JM_Entities;
using BNS.Domain;
using BNS.Resource;
using BNS.Resource.LocalizationResources;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;

namespace BNS.Service.Implement.BaseImplement
{
    public class UpdateRequestHandler<TModel, TEntity> : IRequestHandler<CommandUpdateBase<ApiResult<Guid>>, ApiResult<Guid>> where TEntity : BaseJMEntity
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateRequestHandler(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public virtual IQueryable<TEntity> GetQueryableData(CommandUpdateBase<ApiResult<Guid>> request)
        {
            return _unitOfWork.Repository<TEntity>().Where(s => s.CompanyId == request.CompanyId && s.Id == request.Id).AsQueryable();
        }

        public virtual async Task<ApiResult<Guid>> Handle(CommandUpdateBase<ApiResult<Guid>> request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<Guid>();
            var query = GetQueryableData(request);
            var dataCheck = await query.FirstOrDefaultAsync();
            if (dataCheck == null)
            {
                response.errorCode = EErrorCode.NotExistsData.ToString();
                response.title = LocalizedBackendMessages.MSG_NotExistsData;
                return response;
            }
            _mapper.Map(request, dataCheck);
            dataCheck.UpdatedDate = DateTime.UtcNow;
            dataCheck.UpdatedUserId = request.UserId;
            _unitOfWork.Repository<TEntity>().Update(dataCheck);
            response = await _unitOfWork.SaveChangesAsync();
            response.data = (Guid)dataCheck.GetType().GetProperty("Id").GetValue(dataCheck, null);
            return response;
        }
        
        public static void UpdateEntity<T>(T entity, List<ChangeFieldItem> models)
        {
            foreach (var item in models)
            {
                item.Key = item.Key.Substring(0, 1).ToUpper() + item.Key.Substring(1, item.Key.Length - 1);
                var entityName = entity.GetType().GetProperty(item.Key);
                if (entityName != null && item.Type != EControlType.TransferList && item.Type != EControlType.ListObject)
                {
                    entityName.SetValue(entity, item.Value);
                }
            }
        }


    }
}
