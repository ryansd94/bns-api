using BNS.Data.Entities.JM_Entities;
using BNS.Domain;
using BNS.Domain.Commands;
using BNS.Service.Implement.BaseImplement;

namespace BNS.Service.Features
{
    public class DeleteTaskTypeCommand : DeleteRequestHandler<DeleteTaskTypeRequest, JM_TaskType>
    {
        public DeleteTaskTypeCommand(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
