
using AutoMapper;
using BNS.Data.Entities.JM_Entities;
using BNS.Data.EntityContext;
using BNS.Domain;
using BNS.Domain.Queries;
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
    public class GetUserSuggestQuery : IRequestHandler<GetUserSuggest, ApiResult<UserResponse>>
    {
        protected readonly BNSDbContext _context;
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IUnitOfWork _unitOfWork;

        public GetUserSuggestQuery(BNSDbContext context,
         IStringLocalizer<SharedResource> sharedLocalizer,
         IUnitOfWork unitOfWork)
        {
            _context = context;
            _sharedLocalizer = sharedLocalizer;
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResult<UserResponse>> Handle(GetUserSuggest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<UserResponse>();
            response.data = new UserResponse();

            var query = _unitOfWork.Repository<JM_AccountCompany>().Where(s => !s.IsDelete
               && s.CompanyId == request.CompanyId).OrderBy(d => d.CreatedDate).Select(s => new UserResponseItem
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
