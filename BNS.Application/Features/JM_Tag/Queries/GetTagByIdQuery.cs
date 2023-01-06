
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
using Microsoft.EntityFrameworkCore;

namespace BNS.Service.Features
{
    public class GetTagByIdQuery : IRequestHandler<GetTagByIdRequest, ApiResult<TagResponseItem>>
    {
        private readonly IUnitOfWork _unitOfWork;
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IMapper _mapper;

        public GetTagByIdQuery(
         IUnitOfWork unitOfWork,
         IStringLocalizer<SharedResource> sharedLocalizer,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _sharedLocalizer = sharedLocalizer;
            _mapper = mapper;
        }
        public async Task<ApiResult<TagResponseItem>> Handle(GetTagByIdRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<TagResponseItem>();
            var data = await _unitOfWork.Repository<JM_Tag>().FirstOrDefaultAsync(s => s.Id == request.Id &&
            !s.IsDelete &&
            s.CompanyId == request.CompanyId);
            if (data == null)
            {
                response.errorCode = EErrorCode.NotExistsData.ToString();
                response.title = _sharedLocalizer[LocalizedBackendMessages.MSG_NotExistsData];
                return response;
            }
            var rs = _mapper.Map<TagResponseItem>(data);
            response.data = rs;
            return response;
        }

    }
}
