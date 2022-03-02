using BNS.Data.EntityContext;
using BNS.Resource;
using BNS.Resource.LocalizationResources;
using BNS.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Nest;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;
using BNS.Domain.Commands;

namespace BNS.Service.Features
{
    public class CreateJM_TemplateCommand : IRequestHandler<CreateJM_TemplateRequest, ApiResult<Guid>>
    {
        protected readonly BNSDbContext _context;
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        protected readonly IElasticClient _elasticClient;

        public CreateJM_TemplateCommand(BNSDbContext context,
         IStringLocalizer<SharedResource> sharedLocalizer,
         IElasticClient elasticClient)
        {
            _context = context;
            _sharedLocalizer = sharedLocalizer;
            _elasticClient = elasticClient;
        }
        public async Task<ApiResult<Guid>> Handle(CreateJM_TemplateRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<Guid>();
            var dataCheck = await _context.JM_Templates.Where(s => s.Name.Equals(request.Name)).FirstOrDefaultAsync();
            if (dataCheck != null)
            {
                response.errorCode = EErrorCode.IsExistsData.ToString();
                response.title = _sharedLocalizer[LocalizedBackendMessages.MSG_ExistsData];
                return response;
            }
            var data = new BNS.Data.Entities.JM_Entities.JM_Template
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                CreatedDate = DateTime.UtcNow,
                CreatedUser = request.UserId,
                IssueType = request.IssueType,
                AssigneeIssueStatus = request.AssigneeIssueStatus,
                ReporterIssueStatus = request.ReporterIssueStatus

            };
            var abc = await _elasticClient.IndexDocumentAsync(data);

            await _context.JM_Templates.AddAsync(data);
            await _context.SaveChangesAsync();
            response.data = data.Id;
            return response;
        }

    }
}
