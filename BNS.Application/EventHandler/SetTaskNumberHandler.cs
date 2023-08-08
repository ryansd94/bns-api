using BNS.Service.Subcriber;
using BNS.Data.Entities.JM_Entities;
using BNS.Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BNS.Service.EventHandler
{
    public class SetTaskNumberHandler : IRequestHandler<SetTaskNumberSubcriber>
    {
        private readonly IUnitOfWork _unitOfWork;
        public SetTaskNumberHandler(
         IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(SetTaskNumberSubcriber request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<int>();
            var number = await _unitOfWork.Repository<JM_Task>().AsTracking().Where(s => s.CompanyId == request.CompanyId).MaxAsync(s => s.Number);
            request.Task.Number = number + 1;
            _unitOfWork.Repository<JM_Task>().Update(request.Task);
            await _unitOfWork.SaveChangesAsync();
            return Unit.Value;
        }

    }
}
