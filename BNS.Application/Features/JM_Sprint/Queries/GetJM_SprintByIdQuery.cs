
using BNS.Data.EntityContext;
using BNS.Resource;
using BNS.ViewModels;
using BNS.ViewModels.Responses.Project;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BNS.Application.Features
{
    public class GetJM_SprintByIdQuery
    {
        public class GetJM_SprintByIdRequest : CommandByIdRequest<ApiResult<JM_SprintResponseItem>>
        {
        }
        public class GetJM_SprintByIdRequestHandler : IRequestHandler<GetJM_SprintByIdRequest, ApiResult<JM_SprintResponseItem>>
        {
            protected readonly BNSDbContext _context;
            protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;

            public GetJM_SprintByIdRequestHandler(BNSDbContext context,
             IStringLocalizer<SharedResource> sharedLocalizer)
            {
                _context = context;
                _sharedLocalizer = sharedLocalizer;
            }
            public async Task<ApiResult<JM_SprintResponseItem>> Handle(GetJM_SprintByIdRequest request, CancellationToken cancellationToken)
            {
                var response = new ApiResult<JM_SprintResponseItem>();
                var query = _context.JM_Sprints.Where(s => s.Id == request.Id &&
                !s.IsDelete)
                    .Select(s => new JM_SprintResponseItem
                    {
                        Name = s.Name,
                        Description = s.Description,
                        StartDate = s.StartDate,
                        EndDate = s.EndDate,
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
