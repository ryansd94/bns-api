
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
using Nest;
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
        //public async Task<ApiResult<JM_UserResponse>> Handle(GetJM_UserRequest request, CancellationToken cancellationToken)
        //{
        //    var response = new ApiResult<JM_UserResponse>();
        //    response.data = new JM_UserResponse();

        //    var query = await _unitOfWork.JM_AccountCompanyRepository.GetAsync(s => !s.IsDelete
        //    && s.CompanyId == request.CompanyId
        //    && (string.IsNullOrEmpty(request.keyword) || (!string.IsNullOrEmpty(request.keyword) && s.Email.Contains(request.keyword)))
        //    , s => s.OrderBy(d => d.CreatedDate), s => s.JM_Account);

        //    if (!string.IsNullOrEmpty(request.fieldSort))
        //    {
        //        if (!request.fieldSort.Equals("fullName"))
        //            query = query.OrderBy(request.fieldSort, request.sort);
        //        else
        //        {
        //            Expression<Func<JM_AccountCompany, object>> sort = s => s.JM_Account.FullName;
        //            query = query.OrderBy(sort, request.sort);
        //        }
        //    }
        //    query = query.WhereOr(request.filters);
        //    response.recordsTotal = await query.CountAsync();
        //    query = query.Skip(request.start).Take(request.length);

        //    var rs = await query.Select(d => _mapper.Map<JM_UserResponseItem>(d)).ToListAsync();
        //    response.data.Items = rs;
        //    return response;
        //}
        public async Task<ApiResult<UserResponse>> Handle(Domain.Queries.GetUserRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<UserResponse>();
            response.data = new UserResponse();

            var query = _unitOfWork.Repository<JM_AccountCompany>().Where(s => !s.IsDelete
               && s.CompanyId == request.CompanyId).OrderBy(d => d.CreatedDate).Select(s => new UserResponseItem
               {
                   Email = s.Account != null ? s.Account.Email : string.Empty,
                   Status = s.Status,
                   FullName = s.Account != null ? s.Account.FullName : string.Empty,
                   Id = s.Id,
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
