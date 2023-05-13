using AutoMapper;
using BNS.Data.EntityContext;
using BNS.Domain.Responses;
using BNS.Domain;
using BNS.Data.Entities.JM_Entities;

namespace BNS.Service.Features
{
    public class GetViewPermissionQuery : GetRequestHandler<ViewPermissionResponseItem, SYS_ViewPermission>
    {
        protected readonly BNSDbContext _context;

        public GetViewPermissionQuery(
           IMapper mapper,
           IUnitOfWork unitOfWork) : base(unitOfWork, mapper)
        {
        }
    }
}