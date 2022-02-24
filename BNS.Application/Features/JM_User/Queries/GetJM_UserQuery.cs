
using AutoMapper;
using BNS.Application.Interface;
using BNS.Data.Entities.JM_Entities;
using BNS.Data.EntityContext;
using BNS.Resource;
using BNS.Utilities;
using BNS.ViewModels;
using BNS.ViewModels.Responses;
using BNS.ViewModels.Responses.Category;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Nest;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;

namespace BNS.Application.Features
{
    public class GetJM_UserQuery
    {
        public class GetJM_UserRequest : CommandRequest<ApiResult<JM_UserResponse>>
        {
            public string keyword { get; set; }
        }
        public class GetJM_UserRequestHandler : IRequestHandler<GetJM_UserRequest, ApiResult<JM_UserResponse>>
        {
            protected readonly BNSDbContext _context;
            protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
            private readonly IMapper _mapper;
            private readonly IElasticClient _elasticClient;
            private readonly IUnitOfWork _unitOfWork;

            public GetJM_UserRequestHandler(BNSDbContext context,
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
            public async Task<ApiResult<JM_UserResponse>> Handle(GetJM_UserRequest request, CancellationToken cancellationToken)
            {
                var response = new ApiResult<JM_UserResponse>();
                response.data = new JM_UserResponse();

                var query = await _unitOfWork.JM_AccountCompanyRepository.GetAsync(s => !s.IsDelete
                && s.CompanyId == request.CompanyId
                &&(string.IsNullOrEmpty(request.keyword) || (!string.IsNullOrEmpty(request.keyword) && s.Email.Contains(request.keyword)))
                , s => s.OrderBy(d => d.CreatedDate));
               
                if (!string.IsNullOrEmpty(request.fieldSort))
                    query = Common.OrderBy(query, request.fieldSort, request.sort == ESortEnum.desc.ToString() ? false : true);

                response.recordsTotal = await query.CountAsync();
                query = query.Skip(request.start).Take(request.length);

                var rs = await query.Select(d => _mapper.Map<JM_UserResponseItem>(d.JM_Account)).ToListAsync();
                response.data.Items = rs;
                return response;
            }

        }
    }
}
