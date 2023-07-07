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
    public class GetProjectByIdQuery : GetRequestByIdHandler<ProjectResponseItem, JM_Project, GetProjectByIdRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProjectByIdQuery(
           IStringLocalizer<SharedResource> sharedLocalizer,
           IMapper mapper,
           IUnitOfWork unitOfWork) : base(unitOfWork, sharedLocalizer, mapper)
        {
            _unitOfWork = unitOfWork;
        }

        public override IQueryable<JM_Project> GetQueryableData(GetProjectByIdRequest request)
        {
            return _unitOfWork.Repository<JM_Project>()
                .Include(s => s.Sprints)
                .Include(s => s.JM_ProjectMembers)
                .Include(s => s.JM_ProjectTeams)
                .Where(s => s.CompanyId == request.CompanyId && s.Id == request.Id)
                .AsQueryable();
        }
    }
}
