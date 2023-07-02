using AutoMapper;
using BNS.Data.Entities.JM_Entities;
using BNS.Domain;
using BNS.Domain.Commands;
using BNS.Resource;
using BNS.Service.Implement.BaseImplement;
using Microsoft.Extensions.Localization;
using System;
using System.Linq;

namespace BNS.Service.Features
{
    public class ReadNotifyCommand : UpdateRequestHandler<ReadNotifyRequest, JM_NotifycationUser>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReadNotifyCommand(IUnitOfWork unitOfWork,
            IMapper mapper) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
        }

        public override IQueryable<JM_NotifycationUser> GetQueryableData(CommandUpdateBase<ApiResult<Guid>> request)
        {
            return _unitOfWork.Repository<JM_NotifycationUser>().Where(s => s.Id == request.Id && s.UserReceivedId == request.UserId);
        }
    }
}
