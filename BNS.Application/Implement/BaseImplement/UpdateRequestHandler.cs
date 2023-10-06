using AutoMapper;
using BNS.Data.Entities.JM_Entities;
using BNS.Domain;
using BNS.Resource.LocalizationResources;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;

namespace BNS.Service.Implement.BaseImplement
{
    public class UpdateRequestHandler<TRequest, TEntity> : IRequestHandler<TRequest, ApiResult<Guid>> where TEntity : BaseJMEntity where TRequest : CommandUpdateBase<ApiResult<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateRequestHandler(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public virtual IQueryable<TEntity> GetQueryableData(TRequest request)
        {
            return _unitOfWork.Repository<TEntity>().Where(s => s.CompanyId == request.CompanyId && s.Id == request.Id).AsQueryable();
        }

        public virtual async Task OtherHandle(TRequest request, TEntity entity)
        {

        }
        public virtual async Task<ApiResult<Guid>> Handle(TRequest request, CancellationToken cancellationToken)
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
            UpdateEntity<TEntity>(dataCheck, request.ChangeFields, request.UserId);
            await OtherHandle(request, dataCheck);
            dataCheck.UpdatedDate = DateTime.UtcNow;
            dataCheck.UpdatedUserId = request.UserId;
            _unitOfWork.Repository<TEntity>().Update(dataCheck);
            response = await _unitOfWork.SaveChangesAsync();
            response.data = (Guid)dataCheck.GetType().GetProperty("Id").GetValue(dataCheck, null);
            return response;
        }

        public static void UpdateEntity<T>(T entity, List<ChangeFieldItem> models, Guid userId) where T : BaseJMEntity
        {
            if (models == null || models.Count == 0)
                return;
            var modelEntities = models.Where(s => s.IsEntity == true).ToList();
            if (modelEntities.Count == 0) return;
            foreach (var item in modelEntities)
            {
                item.Key = item.Key.Substring(0, 1).ToUpper() + item.Key.Substring(1, item.Key.Length - 1);
                var entityName = entity.GetType().GetProperty(item.Key);
                var type = entityName.PropertyType;
                if (entityName != null && item.Type != EControlType.TransferList && item.Type != EControlType.ListObject)
                {
                    if (type == typeof(string))
                    {
                        entityName.SetValue(entity, item.Value);
                    }
                    else if (type == typeof(Guid?) || type == typeof(Guid))
                    {
                        if (item.Value != null)
                        {
                            entityName.SetValue(entity, Guid.Parse(item.Value.ToString()));
                        }
                        else
                        {
                            if (type == typeof(Guid?))
                            {
                                entityName.SetValue(entity, null);
                            }
                            else
                            {
                                entityName.SetValue(entity, Guid.Empty);
                            }
                        }
                    }
                    else if (type == typeof(bool?) || type == typeof(bool))
                    {
                        if (item.Value != null)
                        {
                            entityName.SetValue(entity, bool.Parse(item.Value.ToString()));
                        }
                    }
                }
            }
            entity.UpdatedDate = DateTime.UtcNow;
            entity.UpdatedUserId = userId;
        }


    }
}
