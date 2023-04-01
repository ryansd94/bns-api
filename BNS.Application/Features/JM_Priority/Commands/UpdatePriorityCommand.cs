using AutoMapper;
using BNS.Data.Entities.JM_Entities;
using BNS.Domain;
using BNS.Domain.Commands;
using BNS.Resource;
using Microsoft.Extensions.Localization;

namespace BNS.Service.Features
{
    public class UpdatePriorityCommand : Implement.BaseImplement.UpdateRequestHandler<UpdatePriorityRequest, JM_Priority>
    {
        public UpdatePriorityCommand(IUnitOfWork unitOfWork,
            IMapper mapper,
            IStringLocalizer<SharedResource> sharedLocalizer) : base(unitOfWork, mapper,sharedLocalizer)
        {
        }
    }
}
