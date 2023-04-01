using AutoMapper;
using BNS.Resource;
using BNS.Utilities;
using BNS.Domain.Responses;
using BNS.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BNS.Domain.Queries;
using BNS.Data.Entities.JM_Entities;

namespace BNS.Service.Features
{
    public class GetUserAssignQuery : IRequestHandler<GetUserAssignRequest, ApiResult<UserResponse>>
    {
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetUserAssignQuery(
            IStringLocalizer<SharedResource> sharedLocalizer,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _sharedLocalizer = sharedLocalizer;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResult<UserResponse>> Handle(GetUserAssignRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<UserResponse>();
            response.data = new UserResponse();

            var query = _unitOfWork.Repository<JM_AccountCompany>().Where(s => !s.IsDelete
               && s.CompanyId == request.CompanyId && s.Status == Enums.EUserStatus.ACTIVE)
                .OrderBy(d => d.CreatedDate).Select(s => new UserResponseItem
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
