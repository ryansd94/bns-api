using BNS.Data.Entities.JM_Entities;
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
    public class CreateJM_TeamCommand
    {
        public class CreateTeamRequest : CommandBase<ApiResult<Guid>>
        {
            [Required]
            public string Name { get; set; }
            public string Code { get; set; }
            public string Description { get; set; }
            public Guid? ParentId { get; set; }
        }
        public class CreateTeamCommandHandler : IRequestHandler<CreateTeamRequest, ApiResult<Guid>>
        {
            protected readonly BNSDbContext _context;
            protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;

            public CreateTeamCommandHandler(BNSDbContext context,
             IStringLocalizer<SharedResource> sharedLocalizer)
            {
                _context = context;
                _sharedLocalizer = sharedLocalizer;
            }
            public async Task<ApiResult<Guid>> Handle(CreateTeamRequest request, CancellationToken cancellationToken)
            {
                var response = new ApiResult<Guid>();
                var dataCheck = await _context.JM_Teams.Where(s => s.Name.Equals(request.Name) ).FirstOrDefaultAsync();
                if(dataCheck != null)
                {
                    response.errorCode = EErrorCode.IsExistsData.ToString();
                    response.title = _sharedLocalizer[LocalizedBackendMessages.MSG_ExistsData];
                    return response;
                }
                var data = new JM_Team
                {
                    Id = Guid.NewGuid(),
                    Code = request.Code,
                    Name = request.Name,
                    Description = request.Description,
                    ParentId = request.ParentId,
                    CreatedDate = DateTime.UtcNow,
                    CreatedUser = request.CreatedBy,
                };
                await _context.JM_Teams.AddAsync(data);
                await _context.SaveChangesAsync();
                response.data = data.Id;
                return response;
            }

        }
    }
}
