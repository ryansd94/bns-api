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
using BNS.Domain.Queries;
using BNS.Data.Entities.JM_Entities;

namespace BNS.Service.Features
{
    public class GetTemplateQuery : IRequestHandler<GetTemplateRequest, ApiResult<TemplateResponse>>
    {
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetTemplateQuery(
            IStringLocalizer<SharedResource> sharedLocalizer,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _sharedLocalizer = sharedLocalizer;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResult<TemplateResponse>> Handle(GetTemplateRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<TemplateResponse>();
            response.data = new TemplateResponse();

            var query = _unitOfWork.Repository<JM_Template>().Where(s => !s.IsDelete && s.CompanyId == request.CompanyId)
                .OrderBy(d => d.CreatedDate).Select(s => new TemplateResponseItem
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

            if (!request.isGetAll)
                query = query.Skip(request.start).Take(request.length);

            var rs = await query.ToListAsync();
            response.data.Items = rs;
            return response;
        }
    }
}
