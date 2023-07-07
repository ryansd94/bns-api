using AutoMapper;
using BNS.Resource;
using BNS.Domain.Responses;
using BNS.Domain;
using Microsoft.Extensions.Localization;
using BNS.Data.Entities.JM_Entities;
using BNS.Service.Implement;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BNS.Domain.Queries;

namespace BNS.Service.Features
{
    public class GetViewPermissionByIdQuery : GetRequestByIdHandler<ViewPermissionByIdResponse, SYS_ViewPermission, GetViewPermissionByIdRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetViewPermissionByIdQuery(
           IStringLocalizer<SharedResource> sharedLocalizer,
           IMapper mapper,
           IUnitOfWork unitOfWork) : base(unitOfWork, sharedLocalizer, mapper)
        {
            _unitOfWork = unitOfWork;
        }

        public override IQueryable<SYS_ViewPermission> GetQueryableData(GetViewPermissionByIdRequest request)
        {
            var query = _unitOfWork.Repository<SYS_ViewPermission>()
                    .Include(s => s.ViewPermissionObjects)
                    .Include(s => s.ViewPermissionActions)
                    .ThenInclude(s => s.ViewPermissionActionDetails)
                    .Where(s => !s.IsDelete && s.CompanyId == request.CompanyId && s.Id == request.Id)
                    .OrderByDescending(d => d.CreatedDate).AsQueryable();

            return query;
        }
    }
}
