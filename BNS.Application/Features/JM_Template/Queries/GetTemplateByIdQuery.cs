using BNS.Resource;
using BNS.Domain.Responses;
using BNS.Domain;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;
using BNS.Domain.Queries;
using AutoMapper;
using BNS.Data.Entities.JM_Entities;
using Microsoft.EntityFrameworkCore;

namespace BNS.Service.Features
{
    public class GetTemplateByIdQuery : IRequestHandler<GetTemplateByIdRequest, ApiResult<TemplateResponseItem>>
    {
        private readonly IUnitOfWork _unitOfWork;
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IMapper _mapper;

        public GetTemplateByIdQuery(IUnitOfWork unitOfWork,
         IStringLocalizer<SharedResource> sharedLocalizer,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _sharedLocalizer = sharedLocalizer;
            _mapper = mapper;
        }
        public async Task<ApiResult<TemplateResponseItem>> Handle(GetTemplateByIdRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<TemplateResponseItem>();
            var template = await _unitOfWork.Repository<JM_Template>()
                .Include(d => d.TemplateStatus)
                .ThenInclude(d => d.Status).FirstOrDefaultAsync(s => s.Id == request.Id &&
               !s.IsDelete && s.CompanyId == request.CompanyId);

            var rs = _mapper.Map<TemplateResponseItem>(template);
            response.data = rs;
            return response;
        }

    }
}
