
using AutoMapper;
using BNS.Data.EntityContext;
using BNS.Resource;
using BNS.ViewModels;
using BNS.ViewModels.Responses.Category;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BNS.Application.Features
{
    public class GetJM_TeamByIdQuery
    {
        public class GetJM_TeamByIdRequest : CommandByIdRequest<ApiResult<JM_TeamResponseItem>>
        {
        }
        public class GetJM_TeamByIdRequestHandler : IRequestHandler<GetJM_TeamByIdRequest, ApiResult<JM_TeamResponseItem>>
        {
            protected readonly BNSDbContext _context;
            protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
            private readonly IMapper _mapper;

            public GetJM_TeamByIdRequestHandler(BNSDbContext context,
             IStringLocalizer<SharedResource> sharedLocalizer,
                IMapper mapper)
            {
                _context = context;
                _sharedLocalizer = sharedLocalizer;
                _mapper = mapper;
            }
            public async Task<ApiResult<JM_TeamResponseItem>> Handle(GetJM_TeamByIdRequest request, CancellationToken cancellationToken)
            {
                var response = new ApiResult<JM_TeamResponseItem>();
                var query = _context.JM_Teams.Where(s => s.Id == request.Id &&
                !s.IsDelete).Select(s => _mapper.Map<JM_TeamResponseItem>(s));
                var rs = await query.FirstOrDefaultAsync();
                response.data = rs;
                return response;
            }

        }
    }
}
