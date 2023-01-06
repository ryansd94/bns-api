using AutoMapper;
using BNS.Data.Entities.JM_Entities;
using BNS.Domain;
using BNS.Domain.Commands;
using BNS.Resource;
using BNS.Service.Implement.BaseImplement;
using Microsoft.Extensions.Localization;

namespace BNS.Service.Features
{
    public class UpdateTagCommand : UpdateRequestHandler<UpdateTagRequest, JM_Tag>
    {
        public UpdateTagCommand(IUnitOfWork unitOfWork,
            IMapper mapper,
            IStringLocalizer<SharedResource> sharedLocalizer) : base(unitOfWork, mapper,sharedLocalizer)
        {
        }
    }
}
