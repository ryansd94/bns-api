
using AutoMapper;
using BNS.Data.Entities.JM_Entities;
using BNS.Data.EntityContext;
using BNS.Domain;
using BNS.Resource;
using BNS.Utilities;
using BNS.Domain.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Nest;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BNS.Domain.Queries;

namespace BNS.Service.Features
{
    public class GetSYS_FilterConfigQuery : IRequestHandler<GetSYS_FilterConfigRequest, ApiResult<FilterConfigResponse>>
    {
        protected readonly BNSDbContext _context;
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetSYS_FilterConfigQuery(BNSDbContext context,
            IStringLocalizer<SharedResource> sharedLocalizer,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _context = context;
            _mapper = mapper;
            _sharedLocalizer = sharedLocalizer;
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResult<FilterConfigResponse>> Handle(GetSYS_FilterConfigRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<FilterConfigResponse>();
            response.data = new FilterConfigResponse();

            var query = _unitOfWork.Repository<SYS_FilterConfig>().Where(s => !s.IsDelete
               && s.CompanyId == request.CompanyId).OrderBy(d => d.CreatedDate).Select(s => _mapper.Map<FilterConfigResponseItem>(s));

            if (!string.IsNullOrEmpty(request.fieldSort))
            {
                query = query.OrderBy(request.fieldSort, request.sort);
            }
            if (!string.IsNullOrEmpty(request.filters))
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
