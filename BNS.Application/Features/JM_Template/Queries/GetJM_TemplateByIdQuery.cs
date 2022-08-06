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
    public class GetJM_TemplateByIdQuery : IRequestHandler<GetJM_TemplateByIdRequest, ApiResult<JM_TemplateResponseItem>>
    {
        private readonly IUnitOfWork _unitOfWork;
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IMapper _mapper;

        public GetJM_TemplateByIdQuery(IUnitOfWork unitOfWork,
         IStringLocalizer<SharedResource> sharedLocalizer,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _sharedLocalizer = sharedLocalizer;
            _mapper = mapper;
        }
        public async Task<ApiResult<JM_TemplateResponseItem>> Handle(GetJM_TemplateByIdRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<JM_TemplateResponseItem>();
            var query = await _unitOfWork.Repository<JM_Template>().FirstOrDefaultAsync(s => s.Id == request.Id &&
             !s.IsDelete &&s.CompanyId == request.CompanyId);

            var rs = _mapper.Map<JM_TemplateResponseItem>(query);
            response.data = rs;
            return response;
        }

    }
}
