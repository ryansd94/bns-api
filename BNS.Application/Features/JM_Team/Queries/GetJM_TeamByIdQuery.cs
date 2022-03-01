
using AutoMapper;
using BNS.Domain;
using BNS.Resource;
using BNS.Resource.LocalizationResources;
using BNS.ViewModels;
using BNS.ViewModels.Responses;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;

namespace BNS.Application.Features
{
    public class GetJM_TeamByIdQuery
    {
        public class GetJM_TeamByIdRequest : CommandByIdRequest<ApiResult<JM_TeamResponseItem>>
        {
        }
        public class GetJM_TeamByIdRequestHandler : IRequestHandler<GetJM_TeamByIdRequest, ApiResult<JM_TeamResponseItem>>
        {
            private readonly IUnitOfWork _unitOfWork;
            protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
            private readonly IMapper _mapper;

            public GetJM_TeamByIdRequestHandler(
             IUnitOfWork unitOfWork,
             IStringLocalizer<SharedResource> sharedLocalizer,
                IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _sharedLocalizer = sharedLocalizer;
                _mapper = mapper;
            }
            public async Task<ApiResult<JM_TeamResponseItem>> Handle(GetJM_TeamByIdRequest request, CancellationToken cancellationToken)
            {
                var response = new ApiResult<JM_TeamResponseItem>();
                var data = await _unitOfWork.JM_TeamRepository.GetDefaultAsync(s => s.Id == request.Id &&
                !s.IsDelete);
                if (data == null)
                {
                    response.errorCode = EErrorCode.NotExistsData.ToString();
                    response.title = _sharedLocalizer[LocalizedBackendMessages.MSG_NotExistsData];
                    return response;
                }
                var rs = _mapper.Map<JM_TeamResponseItem>(data);
                response.data = rs;
                return response;
            }

        }
    }
}
