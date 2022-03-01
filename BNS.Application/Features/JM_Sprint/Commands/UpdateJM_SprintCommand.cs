using BNS.Data.EntityContext;
using BNS.Resource;
using BNS.Resource.LocalizationResources;
using BNS.Models;
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
    public class UpdateJM_SprintCommand
    {
        public class UpdateJM_SprintCommandHandler : IRequestHandler<UpdateJM_SprintRequest, ApiResult<Guid>>
        {
            protected readonly BNSDbContext _context;
            protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;

            public UpdateJM_SprintCommandHandler(BNSDbContext context,
             IStringLocalizer<SharedResource> sharedLocalizer)
            {
                _context = context;
                _sharedLocalizer = sharedLocalizer;
            }
            public async Task<ApiResult<Guid>> Handle(UpdateJM_SprintRequest request, CancellationToken cancellationToken)
            {
                var response = new ApiResult<Guid>();
                var dataCheck = await _context.JM_Sprints.Where(s => s.Id == request.Id ).FirstOrDefaultAsync();
                if (dataCheck == null)
                {
                    response.errorCode = EErrorCode.NotExistsData.ToString();
                    response.title = _sharedLocalizer[LocalizedBackendMessages.MSG_NotExistsData];
                    return response;
                }

                var checkDuplicate = await _context.JM_Sprints.Where(s => s.Name.Equals(request.Name)  && s.Id != request.Id).FirstOrDefaultAsync();
                if (checkDuplicate != null)
                {
                    response.errorCode = EErrorCode.IsExistsData.ToString();
                    response.title = _sharedLocalizer[LocalizedBackendMessages.MSG_ExistsData];
                    return response;
                }
                dataCheck.StartDate = request.StartDate;
                dataCheck.EndDate = request.EndDate;
                dataCheck.Name = request.Name;
                dataCheck.Description = request.Description;
                dataCheck.UpdatedDate = DateTime.UtcNow;
                dataCheck.UpdatedUser = request.UserId;

                _context.JM_Sprints.Update(dataCheck);
                await _context.SaveChangesAsync();
                response.data = dataCheck.Id;
                return response;
            }

        }
    }
}
