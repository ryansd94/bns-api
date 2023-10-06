using BNS.Domain;
using BNS.Domain.Commands;
using BNS.Data.Entities.JM_Entities;
using BNS.Service.Implement.BaseImplement;

namespace BNS.Service.Features
{
    public class DeletePriorityCommand : DeleteRequestHandler<CommandDeleteRequest, JM_Priority>
    {
        public DeletePriorityCommand(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
