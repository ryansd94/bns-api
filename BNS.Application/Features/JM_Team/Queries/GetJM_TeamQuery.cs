
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

            public GetJM_TeamRequestHandler(BNSDbContext context,
             IStringLocalizer<SharedResource> sharedLocalizer)
            {
                _context = context;
                _sharedLocalizer = sharedLocalizer;
            }
            public async Task<ApiResult<JM_TeamResponse>> Handle(GetJM_TeamRequest request, CancellationToken cancellationToken)
            {
                var response = new ApiResult<JM_TeamResponse>();
                response.data = new JM_TeamResponse();
                var query = _context.JM_Teams.Where(s => !string.IsNullOrEmpty(s.Name) &&
                s.IsDelete == null )
                    .Select(s => new JM_TeamResponseItem
                    {
                        Name = s.Name,
                        Description = s.Description,
                        Id = s.Index,
                        UpdatedDate = s.UpdatedDate,
                        CreatedDate = s.CreatedDate,
                        CreatedUserId = s.CreatedUser,
                        UpdatedUserId = s.UpdatedUser
                    });
                if (request.sortModel != null && request.sortModel.Count > 0)
                {
                    var columnSort = request.sortModel[0].field;
                    var sortType = request.sortModel[0].sort;
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

                if (request.search != null && !string.IsNullOrEmpty(request.search.value))
                {
                    var valueSearch = request.search.value;
                    query = Common.SearchBy(query, new CategoryResponseModel(), valueSearch);

                }
                response.recordsTotal = await query.CountAsync();
                query = query.Skip(request.start).Take(request.length);

                var rs = await query.ToListAsync();
                response.data.Items = rs;
                return response;
            }

        }
    }
}
