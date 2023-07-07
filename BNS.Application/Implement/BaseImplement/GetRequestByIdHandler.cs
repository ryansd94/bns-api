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
    public class GetRequestByIdHandler<TModel, TEntity, TRequest> : IRequestHandler<TRequest, ApiResult<TModel>> where TEntity : BaseJMEntity where TRequest : CommandByIdRequest<ApiResult<TModel>>
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

        public virtual IQueryable<TEntity> GetQueryableData(TRequest request)
        {
            return _unitOfWork.Repository<TEntity>().AsNoTracking().Where(s => s.CompanyId == request.CompanyId && s.Id == request.Id).AsQueryable();
        }
        public async Task<ApiResult<TModel>> Handle(TRequest request, CancellationToken cancellationToken)
        {
            var query = GetQueryableData(request);
            var response = new ApiResult<TModel>();
            var xxx= query.Select(s => _mapper.Map<TModel>(s));
            var data = await 
                xxx.FirstOrDefaultAsync();

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
