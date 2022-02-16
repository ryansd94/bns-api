
using AutoMapper;
using BNS.Application.Interface;
using BNS.Data.Entities.JM_Entities;
using BNS.Data.EntityContext;
using BNS.Resource;
using BNS.Utilities;
using BNS.ViewModels;
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
    public class GetJM_TeamQuery
    {
        public class GetJM_TeamRequest : CommandRequest<ApiResult<JM_TeamResponse>>
        {
        }
        public class GetJM_TeamRequestHandler : IRequestHandler<GetJM_TeamRequest, ApiResult<JM_TeamResponse>>
        {
            protected readonly BNSDbContext _context;
            protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
            private readonly IMapper _mapper;
            private readonly IElasticClient _elasticClient;
            private readonly IGenericRepository<JM_Team> _teamRepository;

            public GetJM_TeamRequestHandler(BNSDbContext context,
             IStringLocalizer<SharedResource> sharedLocalizer,
                IMapper mapper,
             IElasticClient elasticClient,
             IGenericRepository<JM_Team> teamRepository)
            {
                _context = context;
                _mapper = mapper;
                _sharedLocalizer = sharedLocalizer;
                _elasticClient = elasticClient;
                _teamRepository = teamRepository;
            }
            public async Task<ApiResult<JM_TeamResponse>> Handle(GetJM_TeamRequest request, CancellationToken cancellationToken)
            {
                var response = new ApiResult<JM_TeamResponse>();
                response.data = new JM_TeamResponse();

                var query = (await _teamRepository.GetAsync(s => !s.IsDelete && s.CompanyIndex == request.CompanyId, s => s.OrderBy(d => d.Name))).Select(s => new JM_TeamResponseItem
                {
                    Name = s.Name,
                    Id = s.Id,
                    Description = s.Description,
                    ParentId = s.ParentId,
                    TeamParent = s.TeamParent,
                    ParentName = s.TeamParent != null ? s.TeamParent.Name : string.Empty, 
                });
                if (!string.IsNullOrEmpty(request.fieldSort))
                    query = Common.OrderBy(query, request.fieldSort, request.sort == ESortEnum.desc.ToString() ? false : true);
                response.recordsTotal = await query.CountAsync();
                query = query.Skip(request.start).Take(request.length);
                var rs = await query.ToListAsync();
                response.data.Items = rs;
                return response;
            }

        }
    }
}
