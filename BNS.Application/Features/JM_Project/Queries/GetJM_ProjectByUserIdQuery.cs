using BNS.Data.EntityContext;
using BNS.Resource;
using BNS.ViewModels;
using BNS.ViewModels.Responses.Category;
using BNS.ViewModels.Responses.Project;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
namespace BNS.Application.Features 
{
    public class GetJM_ProjectByUserIdQuery
    {
        public class GetJM_ProjectByUserIdRequest : CommandByIdRequest<ApiResult<JM_ProjectResponseItem>>
        {
        }
        public class GetJM_ProjectByUserIdHandler : IRequestHandler<GetJM_ProjectByUserIdRequest, ApiResult<JM_ProjectResponseItem>>
        {
            protected readonly BNSDbContext _context;
            protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;

            public GetJM_ProjectByUserIdHandler(BNSDbContext context,
             IStringLocalizer<SharedResource> sharedLocalizer)
            {
                _context = context;
                _sharedLocalizer = sharedLocalizer;
            }
            public async Task<ApiResult<JM_ProjectResponseItem>> Handle(GetJM_ProjectByUserIdRequest request, CancellationToken cancellationToken)
            {
                var response = new ApiResult<JM_ProjectResponseItem>();
                var query = _context.JM_ProjectMembers.Where(s => s.UserId == request.Id &&
                !s.IsDelete).Include(s => s.JM_Project)
                    .Select(s => new JM_ProjectResponseItem
                    {
                        Name = s.JM_Project.Name,
                        Description = s.JM_Project.Description,
                        Id = s.Id,
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
