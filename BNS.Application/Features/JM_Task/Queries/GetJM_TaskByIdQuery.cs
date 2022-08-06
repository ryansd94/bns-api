
using AutoMapper;
using BNS.Data.EntityContext;
using BNS.Resource;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BNS.Domain.Queries;
using BNS.Domain.Responses;
using BNS.Domain;

namespace BNS.Service.Features
{
    public class GetJM_TaskByIdQuery : IRequestHandler<GetJM_TaskByIdRequest, ApiResult<JM_TaskResponseItem>>
    {
        protected readonly BNSDbContext _context;
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IMapper _mapper;

        public GetJM_TaskByIdQuery(BNSDbContext context,
         IStringLocalizer<SharedResource> sharedLocalizer,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _sharedLocalizer = sharedLocalizer;
        }
        public async Task<ApiResult<JM_TaskResponseItem>> Handle(GetJM_TaskByIdRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<JM_TaskResponseItem>();
            var query = _context.JM_Issues.Where(s => s.Id == request.Id &&
            !s.IsDelete).Select(s => _mapper.Map<JM_TaskResponseItem>(s));
            var rs = await query.FirstOrDefaultAsync();
            response.data = rs;
            return response;
        }

    }
}
