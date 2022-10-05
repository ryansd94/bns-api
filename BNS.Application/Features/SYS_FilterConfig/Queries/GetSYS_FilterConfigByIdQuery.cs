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
using BNS.Data.Entities.JM_Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BNS.Service.Features
{
    public class GetSYS_FilterConfigByIdQuery : IRequestHandler<GetSYS_FilterConfigByIdRequest, ApiResult<SYS_FilterConfigResponseItem>>
    {
        private readonly IUnitOfWork _unitOfWork;
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IMapper _mapper;

        public GetSYS_FilterConfigByIdQuery(
         IUnitOfWork unitOfWork,
         IStringLocalizer<SharedResource> sharedLocalizer,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _sharedLocalizer = sharedLocalizer;
            _mapper = mapper;
        }
        public async Task<ApiResult<SYS_FilterConfigResponseItem>> Handle(GetSYS_FilterConfigByIdRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<SYS_FilterConfigResponseItem>();
            var data = await _unitOfWork.Repository<SYS_FilterConfig>().FirstOrDefaultAsync(s => s.Id == request.Id &&
             !s.IsDelete &&
             s.CompanyId == request.CompanyId);
            if (data == null)
            {
                response.errorCode = EErrorCode.NotExistsData.ToString();
                response.title = _sharedLocalizer[LocalizedBackendMessages.MSG_NotExistsData];
                return response;
            }
            var rs = _mapper.Map<SYS_FilterConfigResponseItem>(data);
            response.data = rs;
            return response;
        }

    }
}
