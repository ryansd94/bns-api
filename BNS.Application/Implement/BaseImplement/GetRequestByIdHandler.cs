using AutoMapper;
using BNS.Data.Entities.JM_Entities;
using BNS.Domain;
using BNS.Resource;
using BNS.Resource.LocalizationResources;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;

namespace BNS.Service.Implement
{
    public class GetRequestByIdHandler<TModel, TEntity> : IRequestHandler<CommandByIdRequest<ApiResult<TModel>>, ApiResult<TModel>> where TEntity : BaseJMEntity
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        public GetRequestByIdHandler(IUnitOfWork unitOfWork,
            IStringLocalizer<SharedResource> sharedLocalizer,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _sharedLocalizer = sharedLocalizer;
        }

        public virtual IQueryable<TEntity> GetQueryableData(CommandByIdRequest<ApiResult<TModel>> request)
        {
            return _unitOfWork.Repository<TEntity>().Where(s => s.CompanyId == request.CompanyId && s.Id == request.Id).AsQueryable();
        }
        public async Task<ApiResult<TModel>> Handle(CommandByIdRequest<ApiResult<TModel>> request, CancellationToken cancellationToken)
        {
            var query = GetQueryableData(request);
            var response = new ApiResult<TModel>();
            var data = await query.Select(s => _mapper.Map<TModel>(s))
                .FirstOrDefaultAsync();

            if (data == null)
            {
                response.errorCode = EErrorCode.NotExistsData.ToString();
                response.title = _sharedLocalizer[LocalizedBackendMessages.MSG_NotExistsData];
                return response;
            }
            response.data = data;
            return response;

        }
    }
}
