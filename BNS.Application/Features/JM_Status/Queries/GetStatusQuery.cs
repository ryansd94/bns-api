
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
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BNS.Domain.Queries;

namespace BNS.Service.Features
{
    public class GetStatusQuery : IRequestHandler<GetStatusRequest, ApiResult<StatusResponse>>
    {
        protected readonly BNSDbContext _context;
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetStatusQuery(BNSDbContext context,
         IStringLocalizer<SharedResource> sharedLocalizer,
            IMapper mapper,
         IUnitOfWork unitOfWork)
        {
            _context = context;
            _mapper = mapper;
            _sharedLocalizer = sharedLocalizer;
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResult<StatusResponse>> Handle(GetStatusRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<StatusResponse>();
            response.data = new StatusResponse();

            var query = _unitOfWork.Repository<JM_Status>().Where(s => !s.IsDelete
               && s.CompanyId == request.CompanyId).OrderBy(d => d.CreatedDate).Select(s => _mapper.Map<StatusResponseItem>(s));

            if (!string.IsNullOrEmpty(request.fieldSort))
            {
                query = query.OrderBy(request.fieldSort, request.sort);
            }
            query = query.WhereOr(request.filters);
            response.recordsTotal = await query.CountAsync();
            if (!request.isGetAll)
                query = query.Skip(request.start).Take(request.length);

            var rs = await query.Select(d => _mapper.Map<StatusResponseItem>(d)).ToListAsync();
            response.data.Items = rs;
            return response;
        }

    }
}
