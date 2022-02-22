
using AutoMapper;
using BNS.Application.Interface;
using BNS.Data.EntityContext;
using BNS.Resource;
using BNS.Resource.LocalizationResources;
using BNS.ViewModels;
using BNS.ViewModels.Responses.Project;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;

namespace BNS.Application.Features
{
    public class GetJM_SprintByIdQuery
    {
        public class GetJM_SprintByIdRequest : CommandByIdRequest<ApiResult<JM_SprintResponseItem>>
        {
        }
        public class GetJM_SprintByIdRequestHandler : IRequestHandler<GetJM_SprintByIdRequest, ApiResult<JM_SprintResponseItem>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;

            public GetJM_SprintByIdRequestHandler(
             IStringLocalizer<SharedResource> sharedLocalizer,
             IUnitOfWork unitOfWork,
                IMapper mapper)
            {
                _sharedLocalizer = sharedLocalizer;
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<ApiResult<JM_SprintResponseItem>> Handle(GetJM_SprintByIdRequest request, CancellationToken cancellationToken)
            {
                var response = new ApiResult<JM_SprintResponseItem>();
                var data = await _unitOfWork.JM_SprintRepository.GetDefaultAsync(s => s.Id == request.Id &&
                 !s.IsDelete);
                if (data == null)
                {
                    response.errorCode = EErrorCode.NotExistsData.ToString();
                    response.title = _sharedLocalizer[LocalizedBackendMessages.MSG_NotExistsData];
                    return response;
                }
                var rs = _mapper.Map<JM_SprintResponseItem>(data);
                response.data = rs;
                return response;
            }

        }
    }
}
