﻿using AutoMapper;
using BNS.Domain;
using BNS.Utilities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;

namespace BNS.Service.Features
{
    public class GetRequestHandler<TModel, TEntity> : IRequestHandler<CommandGetRequest<ApiResultList<TModel>>, ApiResultList<TModel>> where TEntity : class
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetRequestHandler(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public virtual IQueryable<TModel> GetItemData(IQueryable<TEntity> query)
        {
            return query.Select(s => _mapper.Map<TModel>(s));
        }

        public virtual async Task<IQueryable<TEntity>> GetQueryableData(CommandGetRequest<ApiResultList<TModel>> request)
        {
            return _unitOfWork.Repository<TEntity>().AsQueryable();
        }

        public virtual async Task<ApiResultList<TModel>> ReturnData(IQueryable<TEntity> query, CommandGetRequest<ApiResultList<TModel>> request)
        {
            var response = new ApiResultList<TModel>();
            response.data = new DynamicDataItem<TModel>();
            response.recordsTotal = await query.CountAsync();

            if (!request.isGetAll)
                query = query.Skip(request.start).Take(request.length);
            var u = GetItemData(query);
            var rs = await u.ToListAsync();
            response.data.Items = rs;
            return response;
        }

        public async Task<ApiResultList<TModel>> Handle(CommandGetRequest<ApiResultList<TModel>> request, CancellationToken cancellationToken)
        {
            var query = GetQueryableData(request).Result;
            query = query.WhereOr(request.filters);

            if (!string.IsNullOrEmpty(request.fieldSort))
            {
                var columnSort = request.fieldSort;
                var sortType = request.sort;
                if (!string.IsNullOrEmpty(columnSort) && !request.isAdd && !request.isEdit)
                {
                    var sort = request.sort == ESortEnum.desc.ToString() ? " DESC" : " ASC";
                    query = query.OrderBy(columnSort + sort);
                }
            }
            var rs = await ReturnData(query, request);
            return rs;
        }

    }
}
