
using AutoMapper;
using BNS.Data.EntityContext;
using BNS.Resource;
using BNS.Utilities;
using BNS.ViewModels;
using BNS.ViewModels.Responses.Category;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
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

            public GetJM_TeamRequestHandler(BNSDbContext context,
             IStringLocalizer<SharedResource> sharedLocalizer,
                IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
                _sharedLocalizer = sharedLocalizer;
            }
            public async Task<ApiResult<JM_TeamResponse>> Handle(GetJM_TeamRequest request, CancellationToken cancellationToken)
            {
                var response = new ApiResult<JM_TeamResponse>();
                response.data = new JM_TeamResponse();
                var query = _context.JM_Teams.Where(s => !string.IsNullOrEmpty(s.Name) &&
                !s.IsDelete)
                    ;
                if (!string.IsNullOrEmpty(request.fieldSort))
                {
                    var columnSort = request.fieldSort;
                    var sortType = request.sort;
                    if (!string.IsNullOrEmpty(columnSort) && !request.isAdd && !request.isEdit)
                    {
                        columnSort = columnSort[0].ToString().ToUpper() + columnSort.Substring(1, columnSort.Length - 1);
                        query = Common.OrderBy(query, columnSort, sortType == ESortEnum.desc.ToString() ? false : true);

                    }
                }
                if (request.isAdd)
                    query = query.OrderByDescending(s => s.CreatedDate);
                if (request.isEdit)
                    query = query.OrderByDescending(s => s.UpdatedDate);

                response.recordsTotal = await query.CountAsync();
                query = query.Skip(request.start).Take(request.length);

                var rs = await query.Include(s => s.TeamParent).Select(s => _mapper.Map<JM_TeamResponseItem>(s)).ToListAsync();
                response.data.Items = rs;
                return response;
            }

        }
    }
}
