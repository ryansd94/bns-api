using BNS.Data.EntityContext;
using BNS.Resource;
using BNS.Resource.LocalizationResources;
using BNS.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;

namespace BNS.Application.Features
{
    public class UpdateJM_TeamCommand
    {
        public class UpdateJM_TeamRequest : CommandBase<ApiResult<Guid>>
        {
            [Required]
            public string Name { get; set; }
            [Required]
            public Guid Id { get; set; }
            public string Code { get; set; }
            public string Description { get; set; }
            public Guid? ParentId { get; set; }
        }
        public class UpdateJM_TeamCommandHandler : IRequestHandler<UpdateJM_TeamRequest, ApiResult<Guid>>
        {
            protected readonly BNSDbContext _context;
            protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;

            public UpdateJM_TeamCommandHandler(BNSDbContext context,
             IStringLocalizer<SharedResource> sharedLocalizer)
            {
                _context = context;
                _sharedLocalizer = sharedLocalizer;
            }
            public async Task<ApiResult<Guid>> Handle(UpdateJM_TeamRequest request, CancellationToken cancellationToken)
            {
                var response = new ApiResult<Guid>();
                var dataCheck = await _context.JM_Teams.Where(s => s.Id == request.Id ).FirstOrDefaultAsync();
                if (dataCheck == null)
                {
                    response.errorCode = EErrorCode.NotExistsData.ToString();
                    response.title = _sharedLocalizer[LocalizedBackendMessages.MSG_NotExistsData];
                    return response;
                }

                var checkDuplicate = await _context.JM_Teams.Where(s => s.Name.Equals(request.Name)  && s.Id != request.Id).FirstOrDefaultAsync();
                if (checkDuplicate != null)
                {
                    response.errorCode = EErrorCode.IsExistsData.ToString();
                    response.title = _sharedLocalizer[LocalizedBackendMessages.MSG_ExistsData];
                    return response;
                }
                dataCheck.Code = request.Code;
                dataCheck.Name = request.Name;
                dataCheck.Description = request.Description;
                dataCheck.ParentId = request.ParentId;
                dataCheck.UpdatedDate = DateTime.UtcNow;
                dataCheck.UpdatedUser = request.CreatedBy;

                _context.JM_Teams.Update(dataCheck);
                await _context.SaveChangesAsync();
                response.data = dataCheck.Id;
                return response;
            }

        }
    }
}
