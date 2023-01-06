
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
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BNS.Domain.Queries;

namespace BNS.Service.Features
{
    public class GetJM_StatusQuery : IRequestHandler<GetJM_StatusRequest, ApiResult<StatusResponse>>
    {
        protected readonly BNSDbContext _context;
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IMapper _mapper;
        private readonly IElasticClient _elasticClient;
        private readonly IUnitOfWork _unitOfWork;

        public GetJM_StatusQuery(BNSDbContext context,
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
        public async Task<ApiResult<StatusResponse>> Handle(GetJM_StatusRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<StatusResponse>();
            response.data = new StatusResponse();

            var query = _unitOfWork.Repository<JM_Status>().Where(s => !s.IsDelete
               && s.CompanyId == request.CompanyId).OrderBy(d => d.CreatedDate).Select(s => new StatusResponseItem
               {
                   Name = s.Name,
                   Color = s.Color,
                   Id = s.Id,
                   CreatedDate = s.CreatedDate,
               });

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
