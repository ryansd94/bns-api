using BNS.Domain;
using BNS.Domain.Commands;
using BNS.Data.Entities.JM_Entities;
using BNS.Service.Implement.BaseImplement;

namespace BNS.Service.Features
{
    public class DeleteStatusCommand : DeleteRequestHandler<DeleteStatusRequest, JM_Status>
    {
        public DeleteStatusCommand(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
