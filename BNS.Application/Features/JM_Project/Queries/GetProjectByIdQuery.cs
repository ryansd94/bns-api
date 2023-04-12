using AutoMapper;
using BNS.Resource;
using BNS.Domain.Responses;
using BNS.Domain;
using Microsoft.Extensions.Localization;
using BNS.Data.Entities.JM_Entities;
using BNS.Service.Implement;

namespace BNS.Service.Features
{
    public class GetProjectByIdQuery : GetRequestByIdHandler<ProjectResponseItem, JM_Project>
    {
        public GetProjectByIdQuery(
           IStringLocalizer<SharedResource> sharedLocalizer,
           IMapper mapper,
           IUnitOfWork unitOfWork) : base(unitOfWork, sharedLocalizer, mapper)
        {
        }
    }
}
