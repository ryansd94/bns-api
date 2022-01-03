
using BNS.Data.EntityContext;
using BNS.Resource;
using BNS.Utilities;
using BNS.ViewModels;
using BNS.ViewModels.Responses.Category;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;

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

            public GetJM_TeamByIdRequestHandler(BNSDbContext context,
             IStringLocalizer<SharedResource> sharedLocalizer)
            {
                _context = context;
                _sharedLocalizer = sharedLocalizer;
            }
            public async Task<ApiResult<JM_TeamResponseItem>> Handle(GetJM_TeamByIdRequest request, CancellationToken cancellationToken)
            {
                var response = new ApiResult<JM_TeamResponseItem>();
                var query = _context.JM_Teams.Where(s => !string.IsNullOrEmpty(s.Name) &&
                s.IsDelete == null  )
                    .Select(s => new JM_TeamResponseItem
                    {
                        Name = s.Name,
                        Description = s.Description,
                        Id = s.Index,
                        UpdatedDate = s.UpdatedDate,
                        CreatedDate = s.CreatedDate,
                        CreatedUserId = s.CreatedUser,
                        UpdatedUserId = s.UpdatedUser
                    });
                var rs = await query.FirstOrDefaultAsync();
                response.data = rs;
                return response;
            }

        }
    }
}
