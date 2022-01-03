using BNS.Data.EntityContext;
using BNS.Resource;
using BNS.Resource.LocalizationResources;
using BNS.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;
namespace BNS.Application.Features
{
    public class DeleteJM_TeamCommand
    {
        public class DeleteJM_TeamRequest : CommandBase<ApiResult<Guid>>
        {
            [Required]
            public List<Guid> ids { get; set; }
        }
        public class DeleteJM_TeamCommandHandler : IRequestHandler<DeleteJM_TeamRequest, ApiResult<Guid>>
        {
            protected readonly BNSDbContext _context;
            protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;

            public DeleteJM_TeamCommandHandler(BNSDbContext context,
             IStringLocalizer<SharedResource> sharedLocalizer)
            {
                _context = context;
                _sharedLocalizer = sharedLocalizer;
            }
            public async Task<ApiResult<Guid>> Handle(DeleteJM_TeamRequest request, CancellationToken cancellationToken)
            {
                var response = new ApiResult<Guid>();
                var dataChecks = await _context.JM_Teams.Where(s => request.ids.Contains(s.Index)).ToListAsync();
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
                    item.UpdatedUser = request.CreatedBy;
                    _context.JM_Teams.Update(item);
                }
                await _context.SaveChangesAsync();
                return response;
            }

        }
    }
}
