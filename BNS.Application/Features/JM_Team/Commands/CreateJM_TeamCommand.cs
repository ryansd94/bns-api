using BNS.Data.Entities.JM_Entities;
using BNS.Domain;
using BNS.Resource;
using BNS.Resource.LocalizationResources;
using MediatR;
using Microsoft.Extensions.Localization;
using Nest;
using System;
using System.Threading;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;
using BNS.Domain.Commands;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BNS.Service.Features
{
    public class CreateJM_TeamCommand : IRequestHandler<CreateJM_TeamRequest, ApiResult<Guid>>
    {
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        protected readonly IElasticClient _elasticClient;
        private readonly IUnitOfWork _unitOfWork;
        public CreateJM_TeamCommand(
         IStringLocalizer<SharedResource> sharedLocalizer,
         IElasticClient elasticClient,
         IUnitOfWork unitOfWork)
        {
            _sharedLocalizer = sharedLocalizer;
            _elasticClient = elasticClient;
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResult<Guid>> Handle(CreateJM_TeamRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<Guid>();
            var dataCheck = await _unitOfWork.JM_TeamRepository.FirstOrDefaultAsync(s => s.Name.Equals(request.Name) && s.CompanyId == request.CompanyId);
            if (dataCheck != null)
            {
                response.errorCode = EErrorCode.IsExistsData.ToString();
                response.title = _sharedLocalizer[LocalizedBackendMessages.MSG_ExistsData];
                return response;
            }
            var team = new JM_Team
            {
                Id = Guid.NewGuid(),
                Code = request.Code,
                Name = request.Name,
                Description = request.Description,
                ParentId = request.ParentId,
                CreatedDate = DateTime.UtcNow,
                CreatedUser = request.UserId,
                CompanyId = request.CompanyId,
                IsDelete = false
            };
            await _unitOfWork.JM_TeamRepository.AddAsync(team);
            if (request.Members != null && request.Members.Count > 0)
            {
                var accountCompanys = await _unitOfWork.Repository<JM_AccountCompany>().Where(s => request.Members.Contains(s.Id) &&
                  !s.IsDelete).ToListAsync();
                foreach (var account in accountCompanys)
                {
                    account.TeamId = team.Id;
                    _unitOfWork.Repository<JM_AccountCompany>().Update(account);
                }
            }
            response = await _unitOfWork.SaveChangesAsync();

            //_elasticClient.Index<JM_Team>(data, i => i
            //       .Index("bns")
            //       .Id(data.Id)
            //       .Refresh(Elasticsearch.Net.Refresh.True));
            //var abc = await _elasticClient.IndexDocumentAsync(data);
            //var abc = await _elasticClient.UpdateAsync<JM_Team>(data, u => u.Doc(data));
            return response;
        }

    }
}
