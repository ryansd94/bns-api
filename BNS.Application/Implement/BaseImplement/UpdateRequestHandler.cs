using AutoMapper;
using BNS.Data.Entities.JM_Entities;
using BNS.Domain;
using BNS.Resource;
using BNS.Resource.LocalizationResources;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;

namespace BNS.Service.Implement.BaseImplement
{
    public class UpdateRequestHandler<TModel, TEntity> : IRequestHandler<CommandUpdateBase<ApiResult<Guid>>, ApiResult<Guid>> where TEntity : BaseJMEntity
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        public UpdateRequestHandler(IUnitOfWork unitOfWork,
            IMapper mapper,
            IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _sharedLocalizer = sharedLocalizer;
        }

        public async Task<ApiResult<Guid>> Handle(CommandUpdateBase<ApiResult<Guid>> request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<Guid>();
            var dataCheck = await _unitOfWork.Repository<TEntity>().Where(s => s.Id == request.Id && s.CompanyId == request.CompanyId).FirstOrDefaultAsync();
            if (dataCheck == null)
            {
                response.errorCode = EErrorCode.NotExistsData.ToString();
                response.title = _sharedLocalizer[LocalizedBackendMessages.MSG_NotExistsData];
                return response;
            }
            _mapper.Map(request, dataCheck);
            dataCheck.UpdatedDate = DateTime.UtcNow;
            dataCheck.UpdatedUserId = request.UserId;
            _unitOfWork.Repository<TEntity>().Update(dataCheck);
            response = await _unitOfWork.SaveChangesAsync();
            response.data = (Guid)dataCheck.GetType().GetProperty("Id").GetValue(dataCheck, null);
            return response;
        }
    }
}
