
using AutoMapper;
using BNS.Data.EntityContext;
using BNS.Resource;
using BNS.Models;
using BNS.Models.Responses.Project;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BNS.Domain.Queries;

namespace BNS.Service.Features
{
    public class GetJM_IssueByIdQuery : IRequestHandler<GetJM_IssueByIdRequest, ApiResult<JM_IssueResponseItem>>
    {
        protected readonly BNSDbContext _context;
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IMapper _mapper;

        public GetJM_IssueByIdQuery(BNSDbContext context,
         IStringLocalizer<SharedResource> sharedLocalizer,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _sharedLocalizer = sharedLocalizer;
        }
        public async Task<ApiResult<JM_IssueResponseItem>> Handle(GetJM_IssueByIdRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<JM_IssueResponseItem>();
            var query = _context.JM_Issues.Where(s => s.Id == request.Id &&
            !s.IsDelete).Select(s => _mapper.Map<JM_IssueResponseItem>(s));
            var rs = await query.FirstOrDefaultAsync();
            response.data = rs;
            return response;
        }

    }
}
