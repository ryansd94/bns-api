
using AutoMapper;
using BNS.Data.Entities.JM_Entities;
using BNS.Data.EntityContext;
using BNS.Domain;
using BNS.Domain.Responses;
using BNS.Resource;
using BNS.Utilities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;

namespace BNS.Service.Features
{
    public class GetUserQuery : IRequestHandler<Domain.Queries.GetUserRequest, ApiResult<UserResponse>>
    {
        protected readonly BNSDbContext _context;
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetUserQuery(BNSDbContext context,
         IStringLocalizer<SharedResource> sharedLocalizer,
            IMapper mapper,
         IUnitOfWork unitOfWork)
        {
            _context = context;
            _mapper = mapper;
            _sharedLocalizer = sharedLocalizer;
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResult<UserResponse>> Handle(Domain.Queries.GetUserRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<UserResponse>();
            response.data = new UserResponse();

            var query = _unitOfWork.Repository<JM_AccountCompany>().Where(s => !s.IsDelete
               && s.CompanyId == request.CompanyId).Include(s => s.JM_Team).OrderBy(d => d.CreatedDate).Select(s => new UserResponseItem
               {
                   Email = s.Account != null ? s.Account.Email : string.Empty,
                   Status = s.Status,
                   FullName = s.Account != null ? s.Account.FullName : string.Empty,
                   Id = s.Account.Id,
                   CreatedDate = s.CreatedDate,
                   IsMainAccount = s.IsMainAccount,
                   TeamName = s.JM_Team != null ? s.JM_Team.Name : string.Empty,
                   TeamId = s.TeamId,
                   Image = s.Account != null ? s.Account.Image : string.Empty,
                   UserId = s.Id
               });

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
