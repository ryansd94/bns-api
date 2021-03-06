using AutoMapper;
using BNS.Data.EntityContext;
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
using static BNS.Utilities.Enums;
using BNS.Domain.Queries;

namespace BNS.Service.Features
{
    public class GetJM_TemplateQuery : IRequestHandler<GetJM_TemplateRequest, ApiResult<JM_TemplateResponse>>
    {
        protected readonly BNSDbContext _context;
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IMapper _mapper;

        public GetJM_TemplateQuery(BNSDbContext context,
         IStringLocalizer<SharedResource> sharedLocalizer,
            IMapper mapper)
        {
            _context = context;
            _sharedLocalizer = sharedLocalizer;
            _mapper = mapper;
        }
        public async Task<ApiResult<JM_TemplateResponse>> Handle(GetJM_TemplateRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<JM_TemplateResponse>();
            response.data = new JM_TemplateResponse();
            var query = _context.JM_Templates.Where(s =>!s.IsDelete &&s.CompanyId == request.CompanyId);
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

            var rs = await query.Select(s => _mapper.Map<JM_TemplateResponseItem>(s)).ToListAsync();
            response.data.Items = rs;
            return response;
        }

    }
}
