
using AutoMapper;
using BNS.Data.Entities.JM_Entities;
using BNS.Data.EntityContext;
using BNS.Domain;
using BNS.Resource;
using BNS.Utilities;
using BNS.Domain.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Nest;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;
using BNS.Domain.Queries;

namespace BNS.Service.Features
{
    public class GetJM_TeamQuery : IRequestHandler<GetJM_TeamRequest, ApiResult<JM_TeamResponse>>
    {
        protected readonly BNSDbContext _context;
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IMapper _mapper;
        private readonly IElasticClient _elasticClient;
        private readonly IUnitOfWork _unitOfWork;

        public GetJM_TeamQuery(BNSDbContext context,
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
        public async Task<ApiResult<JM_TeamResponse>> Handle(GetJM_TeamRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<JM_TeamResponse>();
            response.data = new JM_TeamResponse();
            Expression<Func<JM_Team, bool>> filter = s => !s.IsDelete && s.CompanyId == request.CompanyId;

            var query = (await _unitOfWork.JM_TeamRepository.GetAsync(filter,
                s => s.OrderBy(d => d.Name) ,s=>s.TeamParent)).Select(s => _mapper.Map<JM_TeamResponseItem>(s));
            if (!string.IsNullOrEmpty(request.fieldSort))
                query = Common.OrderBy(query, request.fieldSort, request.sort == ESortEnum.desc.ToString() ? false : true);
            response.recordsTotal = await _unitOfWork.JM_TeamRepository.CountAsync(filter);
            query = query.Skip(request.start).Take(request.length);
            var rs = await query.ToListAsync();
            response.data.Items = rs;
            return response;
        }

    }
}
