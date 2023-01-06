
using AutoMapper;
using BNS.Domain;
using BNS.Resource;
using BNS.Resource.LocalizationResources;
using BNS.Domain.Responses;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;
using BNS.Domain.Queries;

namespace BNS.Service.Features
{
    public class GetJM_SprintByIdQuery : IRequestHandler<GetJM_SprintByIdRequest, ApiResult<SprintResponseItem>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public GetJM_SprintByIdQuery(
         IStringLocalizer<SharedResource> sharedLocalizer,
         IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _sharedLocalizer = sharedLocalizer;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ApiResult<SprintResponseItem>> Handle(GetJM_SprintByIdRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<SprintResponseItem>();
            var data = await _unitOfWork.JM_SprintRepository.FirstOrDefaultAsync(s => s.Id == request.Id &&
             !s.IsDelete);
            if (data == null)
            {
                response.errorCode = EErrorCode.NotExistsData.ToString();
                response.title = _sharedLocalizer[LocalizedBackendMessages.MSG_NotExistsData];
                return response;
            }
            var rs = _mapper.Map<SprintResponseItem>(data);
            response.data = rs;
            return response;
        }

    }
}
