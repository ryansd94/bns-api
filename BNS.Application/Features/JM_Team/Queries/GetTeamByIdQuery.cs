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
    public class GetTeamByIdQuery : IRequestHandler<GetTeamByIdRequest, ApiResult<TeamResponseItemById>>
    {
        private readonly IUnitOfWork _unitOfWork;
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IMapper _mapper;

        public GetTeamByIdQuery(
         IUnitOfWork unitOfWork,
         IStringLocalizer<SharedResource> sharedLocalizer,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _sharedLocalizer = sharedLocalizer;
            _mapper = mapper;
        }
        public async Task<ApiResult<TeamResponseItemById>> Handle(GetTeamByIdRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<TeamResponseItemById>();
            var data = await _unitOfWork.Repository<JM_Team>()
                .Include(s => s.Members).ThenInclude(s => s.Account).FirstOrDefaultAsync(s => s.Id == request.Id &&
                !s.IsDelete &&
                s.CompanyId == request.CompanyId);
            if (data == null)
            {
                response.errorCode = EErrorCode.NotExistsData.ToString();
                response.title = _sharedLocalizer[LocalizedBackendMessages.MSG_NotExistsData];
                return response;
            }
            var rs = _mapper.Map<TeamResponseItemById>(data);
            response.data = rs;
            return response;
        }

    }
}
