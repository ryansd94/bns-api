﻿using AutoMapper;
using BNS.Data.Entities.JM_Entities;
using BNS.Data.EntityContext;
using BNS.Domain;
using BNS.Domain.Queries;
using BNS.Domain.Responses;
using BNS.Resource;
using BNS.Utilities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BNS.Service.Features
{
    public class GetTaskTypeQuery : IRequestHandler<GetTaskTypeRequest, ApiResult<TaskTypeResponse>>
    {
        protected readonly BNSDbContext _context;
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetTaskTypeQuery(BNSDbContext context,
            IStringLocalizer<SharedResource> sharedLocalizer,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _context = context;
            _sharedLocalizer = sharedLocalizer;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResult<TaskTypeResponse>> Handle(GetTaskTypeRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<TaskTypeResponse>();
            response.data = new TaskTypeResponse();

            var query = _unitOfWork.Repository<JM_TaskType>().Where(s => !s.IsDelete && s.CompanyId == request.CompanyId)
                .OrderBy(d => d.CreatedDate).Select(s => _mapper.Map<TaskTypeItem>(s));
            if (!string.IsNullOrEmpty(request.fieldSort))
            {
                var columnSort = request.fieldSort;
                var sortType = request.sort;
                if (!string.IsNullOrEmpty(columnSort) && !request.isAdd && !request.isEdit)
                {
                    columnSort = columnSort[0].ToString().ToUpper() + columnSort.Substring(1, columnSort.Length - 1);
                    query = query.OrderBy(request.fieldSort, request.sort);

                }
            }

            if (!string.IsNullOrEmpty(request.fieldSort))
            {
                query = query.OrderBy(request.fieldSort, request.sort);
            }

            query = query.WhereOr(request.filters);
            response.recordsTotal = await query.CountAsync();
            if (!request.isGetAll)
                query = query.Skip(request.start).Take(request.length);

            var rs = await query.ToListAsync();
            response.data.Items = rs;
            return response;
        }

    }
}
