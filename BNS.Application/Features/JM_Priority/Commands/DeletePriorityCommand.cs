using BNS.Domain;
using BNS.Resource;
using Microsoft.Extensions.Localization;
using BNS.Domain.Commands;
using BNS.Data.Entities.JM_Entities;
using BNS.Service.Implement.BaseImplement;

namespace BNS.Service.Features
{
    public class DeletePriorityCommand : DeleteRequestHandler<DeletePriorityRequest, JM_Priority>
    {
        public DeletePriorityCommand(IUnitOfWork unitOfWork,
            IStringLocalizer<SharedResource> sharedLocalizer) : base(unitOfWork, sharedLocalizer)
        {
        }
    }
}
