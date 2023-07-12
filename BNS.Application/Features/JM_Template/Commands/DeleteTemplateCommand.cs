using BNS.Data.Entities.JM_Entities;
using BNS.Domain;
using BNS.Domain.Commands;
using BNS.Service.Implement.BaseImplement;

namespace BNS.Service.Features
{
    public class DeleteTemplateCommand : DeleteRequestHandler<DeleteTemplateRequest, JM_Template>
    {
        public DeleteTemplateCommand(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
