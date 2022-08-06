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
using BNS.Data.Entities.JM_Entities;

namespace BNS.Service.Features
{
    public class GetJM_TemplateQuery : IRequestHandler<GetJM_TemplateRequest, ApiResult<JM_TemplateResponse>>
    {
        protected readonly BNSDbContext _context;
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetJM_TemplateQuery(BNSDbContext context,
            IStringLocalizer<SharedResource> sharedLocalizer,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _context = context;
            _sharedLocalizer = sharedLocalizer;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResult<JM_TemplateResponse>> Handle(GetJM_TemplateRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<JM_TemplateResponse>();
            response.data = new JM_TemplateResponse();

            var query = _unitOfWork.Repository<JM_Template>().Where(s => !s.IsDelete && s.CompanyId == request.CompanyId)
                .OrderBy(d => d.CreatedDate).Select(s => new JM_TemplateResponseItem
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description,
                    CreatedDate = s.CreatedDate
                });
            if (!string.IsNullOrEmpty(request.fieldSort))
            {
                var columnSort = request.fieldSort;
                var sortType = request.sort;
                if (!string.IsNullOrEmpty(columnSort) && !request.isAdd && !request.isEdit)
                {
                    columnSort = columnSort[0].ToString().ToUpper() + columnSort.Substring(1, columnSort.Length - 1);
                    query = query.OrderBy(request.fieldSort, request.sort);

                }
            }

            if (!string.IsNullOrEmpty(request.fieldSort))
            {
                query = query.OrderBy(request.fieldSort, request.sort);
            }
            query = query.WhereOr(request.filters);
            response.recordsTotal = await query.CountAsync();
            query = query.Skip(request.start).Take(request.length);

            var rs = await query.ToListAsync();
            response.data.Items = rs;
            return response;
        }

    }
}
