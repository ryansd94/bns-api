using AutoMapper;
using BNS.Domain;
using BNS.Resource;
using BNS.Resource.LocalizationResources;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using static BNS.Utilities.Common;
using static BNS.Utilities.Enums;

namespace BNS.Service.Implement.BaseImplement
{
    public class UpdateRequestHandler<TModel, TEntity> : IRequestHandler<CommandUpdateBase<ApiResult<Guid>>, ApiResult<Guid>> where TEntity : class
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
            var filters = new List<SearchCriteria>
            {
                new SearchCriteria
                {
                    Column="Id",
                    Value=request.Id,
                    Condition=EWhereCondition.Equal,
                },
                new SearchCriteria
                {
                    Column="CompanyId",
                    Value=request.CompanyId,
                    Condition=EWhereCondition.Equal,
                }
            };

            var dataCheck = await _unitOfWork.Repository<TEntity>().WhereOr(filters).FirstOrDefaultAsync();
            if (dataCheck == null)
            {
                response.errorCode = EErrorCode.NotExistsData.ToString();
                response.title = _sharedLocalizer[LocalizedBackendMessages.MSG_NotExistsData];
                return response;
            }
            _mapper.Map(request, dataCheck);
            dataCheck.GetType().GetProperty("UpdatedDate").SetValue(dataCheck,DateTime.UtcNow);
            dataCheck.GetType().GetProperty("UpdatedUserId").SetValue(dataCheck,request.UserId);
            _unitOfWork.Repository<TEntity>().Update(dataCheck);
            response = await _unitOfWork.SaveChangesAsync();
            response.data = (Guid)dataCheck.GetType().GetProperty("Id").GetValue(dataCheck, null);
            return response;
        }
    }
}
