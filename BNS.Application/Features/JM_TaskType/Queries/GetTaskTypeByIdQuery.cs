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
    public class GetTaskTypeByIdQuery : IRequestHandler<GetTaskTypeByIdRequest, ApiResult<TaskTypeItem>>
    {
        private readonly IUnitOfWork _unitOfWork;
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IMapper _mapper;

        public GetTaskTypeByIdQuery(IUnitOfWork unitOfWork,
         IStringLocalizer<SharedResource> sharedLocalizer,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _sharedLocalizer = sharedLocalizer;
            _mapper = mapper;
        }
        public async Task<ApiResult<TaskTypeItem>> Handle(GetTaskTypeByIdRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<TaskTypeItem>();
            var query = await _unitOfWork.Repository<JM_TaskType>()
                .Include(s=>s.Template)
                .ThenInclude(s=>s.TemplateStatus)
                .ThenInclude(s => s.Status)
                .FirstOrDefaultAsync(s => s.Id == request.Id &&
             !s.IsDelete && s.CompanyId == request.CompanyId);

            var rs = _mapper.Map<TaskTypeItem>(query);
            response.data = rs;
            return response;
        }

    }
}
