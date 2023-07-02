using AutoMapper;
using BNS.Data.Entities.JM_Entities;
using BNS.Domain;
using BNS.Resource;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BNS.Service.Implement.BaseImplement
{
    public class CreateRequestHandler<TModel, TEntity> : IRequestHandler<CommandCreateBase<ApiResultList<Guid>>, ApiResultList<Guid>> where TEntity : BaseJMEntity
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        public CreateRequestHandler(IUnitOfWork unitOfWork,
            IMapper mapper,
            IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _sharedLocalizer = sharedLocalizer;
        }

        public async Task<ApiResultList<Guid>> Handle(CommandCreateBase<ApiResultList<Guid>> request, CancellationToken cancellationToken)
        {
            var response = new ApiResultList<Guid>();
            var dataInserts = new List<TEntity>();
            foreach (var item in request.Items)
            {
                var data = _mapper.Map<TEntity>(item);
                data.UpdatedDate = DateTime.UtcNow;
                data.UpdatedUserId = request.UserId;
                dataInserts.Add(data);
            }
            await _unitOfWork.Repository<TEntity>().AddRangeAsync(dataInserts);
            await _unitOfWork.SaveChangesAsync();
            response.data.Items = dataInserts.Select(s => s.Id).ToList();
            return response;
        }
    }
}
