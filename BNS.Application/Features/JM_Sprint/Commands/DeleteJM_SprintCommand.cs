using BNS.Data.EntityContext;
using BNS.Resource;
using BNS.Resource.LocalizationResources;
using BNS.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;
using BNS.Domain.Commands;

namespace BNS.Service.Features
{
    public class DeleteJM_SprintCommand : IRequestHandler<DeleteJM_SprintRequest, ApiResult<Guid>>
    {
        protected readonly BNSDbContext _context;
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public DeleteJM_SprintCommand(BNSDbContext context,
         IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _context = context;
            _sharedLocalizer = sharedLocalizer;
        }
        public async Task<ApiResult<Guid>> Handle(DeleteJM_SprintRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<Guid>();
            var dataChecks = await _context.JM_Sprints.Where(s => request.ids.Contains(s.Id)).ToListAsync();
            if (dataChecks == null || dataChecks.Count() ==0)
            {
                response.errorCode = EErrorCode.NotExistsData.ToString();
                response.title = _sharedLocalizer[LocalizedBackendMessages.MSG_NotExistsData];
                return response;
            }
            foreach (var item in dataChecks)
            {
                item.IsDelete = true;
                item.UpdatedDate = DateTime.UtcNow;
                item.UpdatedUser = request.UserId;
                _context.JM_Sprints.Update(item);
            }
            await _context.SaveChangesAsync();
            return response;
        }

    }
}
