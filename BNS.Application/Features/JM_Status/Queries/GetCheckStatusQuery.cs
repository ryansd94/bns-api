using AutoMapper;
using BNS.Data.Entities.JM_Entities;
using BNS.Data.EntityContext;
using BNS.Domain;
using BNS.Resource;
using BNS.Domain.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Nest;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BNS.Domain.Queries;

namespace BNS.Service.Features
{
    public class GetCheckStatusQuery : IRequestHandler<GetCheckStatusRequest, ApiResult<StatusCheckResponse>>
    {
        protected readonly BNSDbContext _context;
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IMapper _mapper;
        private readonly IElasticClient _elasticClient;
        private readonly IUnitOfWork _unitOfWork;

        public GetCheckStatusQuery(BNSDbContext context,
         IStringLocalizer<SharedResource> sharedLocalizer,
            IMapper mapper,
         IElasticClient elasticClient,
         IUnitOfWork unitOfWork)
        {
            _context = context;
            _mapper = mapper;
            _sharedLocalizer = sharedLocalizer;
            _elasticClient = elasticClient;
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResult<StatusCheckResponse>> Handle(GetCheckStatusRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<StatusCheckResponse>();
            response.data = new StatusCheckResponse();

            var statusCheck = await _unitOfWork.Repository<JM_Status>().Where(s => !s.IsDelete
                && s.CompanyId == request.CompanyId && (s.IsStatusStart || s.IsStatusEnd)).ToListAsync();

            response.data.IsStatusStart = statusCheck.Any(s => s.IsStatusStart);
            response.data.IsStatusEnd = statusCheck.Any(s => s.IsStatusEnd);
            return response;
        }

    }
}
