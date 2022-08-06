
using AutoMapper;
using BNS.Data.EntityContext;
using BNS.Domain;
using BNS.Resource;
using BNS.Utilities;
using BNS.Domain.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Nest;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;
using BNS.Domain.Queries;
using BNS.Data.Entities.JM_Entities;
using System.Linq.Expressions;
using System;
using System.Linq.Dynamic.Core;

namespace BNS.Service.Features
{
    public class GetJM_UserQuery : IRequestHandler<GetJM_UserRequest, ApiResult<JM_UserResponse>>
    {
        protected readonly BNSDbContext _context;
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IMapper _mapper;
        private readonly IElasticClient _elasticClient;
        private readonly IUnitOfWork _unitOfWork;

        public GetJM_UserQuery(BNSDbContext context,
         IStringLocalizer<SharedResource> sharedLocalizer,
            IMapper mapper,
         IElasticClient elasticClient,
         IUnitOfWork unitOfWork)
        {
            _context = context;
            _mapper = mapper;
            _sharedLocalizer = sharedLocalizer;
            _elasticClient = elasticClient;
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
        public async Task<ApiResult<JM_UserResponse>> Handle(GetJM_UserRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<JM_UserResponse>();
            response.data = new JM_UserResponse();

            var query = _unitOfWork.Repository<JM_AccountCompany>().Where(s => !s.IsDelete
           && s.CompanyId == request.CompanyId).OrderBy(d => d.CreatedDate).Select(s => new JM_UserResponseItem
           {
               Email = s.Email,
               Status = s.Status,
               FullName = s.JM_Account != null ? s.JM_Account.FullName : string.Empty,
               Id = s.Id,
               CreatedDate = s.CreatedDate,
               IsMainAccount = s.IsMainAccount,
               TeamName = s.JM_Team != null ? s.JM_Team.Name : string.Empty,
               TeamId = s.TeamId,
               Image = s.JM_Account != null ? s.JM_Account.Image : string.Empty,
           });

            if (!string.IsNullOrEmpty(request.fieldSort))
            {
                query = query.OrderBy(request.fieldSort, request.sort);
            }
            query = query.WhereOr(request.filters);
            response.recordsTotal = await query.CountAsync();
            query = query.Skip(request.start).Take(request.length);

            var rs = await query.Select(d => _mapper.Map<JM_UserResponseItem>(d)).ToListAsync();
            response.data.Items = rs;
            return response;
        }

    }
}
