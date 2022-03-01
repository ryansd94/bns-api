using BNS.Service.Subcriber;
using BNS.Data.Entities.JM_Entities;
using BNS.Domain;
using BNS.Resource;
using BNS.Resource.LocalizationResources;
using BNS.Models;
using MediatR;
using Microsoft.Extensions.Localization;
using Nest;
using System;
using System.Threading;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;
namespace BNS.Service.EventHandler
{
    public class CreateJM_TeamHandler : IRequestHandler<CreateJM_TeamSubcriberMQ>
    {
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        protected readonly IElasticClient _elasticClient;
        private readonly IUnitOfWork _unitOfWork;
        public CreateJM_TeamHandler(
         IStringLocalizer<SharedResource> sharedLocalizer,
         IElasticClient elasticClient,
         IUnitOfWork unitOfWork)
        {
            _sharedLocalizer = sharedLocalizer;
            _elasticClient = elasticClient;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(CreateJM_TeamSubcriberMQ request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<Guid>();
            var dataCheck = await _unitOfWork.JM_TeamRepository.GetDefaultAsync(s => s.Name.Equals(request.Name) && s.CompanyIndex == request.CompanyId);
            if (dataCheck != null)
            {
                response.errorCode = EErrorCode.IsExistsData.ToString();
                response.title = _sharedLocalizer[LocalizedBackendMessages.MSG_ExistsData];
                return Unit.Value;
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
                CompanyIndex=request.CompanyId
            };
            if (request.Members != null && request.Members.Count>0)
            {
                foreach (var item in request.Members)
                {
                    await _unitOfWork.JM_TeamMemberRepository.AddAsync(new JM_TeamMember
                    {
                        CompanyIndex=request.CompanyId,
                        CreatedDate=DateTime.UtcNow,
                        CreatedUser=request.CreatedBy,
                        Id=Guid.NewGuid(),
                        IsDelete=false,
                        TeamId=data.Id,
                        UserId=item
                    });
                }
            }
            await _unitOfWork.JM_TeamRepository.AddAsync(data);
            response= await _unitOfWork.SaveChangesAsync();
            //_elasticClient.Index<JM_Team>(data, i => i
            //       .Index("bns")
            //       .Id(data.Id)
            //       .Refresh(Elasticsearch.Net.Refresh.True));
            //var abc = await _elasticClient.IndexDocumentAsync(data);
            //var abc = await _elasticClient.UpdateAsync<JM_Team>(data, u => u.Doc(data));
            return Unit.Value;
        }

    }
}
