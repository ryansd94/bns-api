using AutoMapper;
using BNS.Data.EntityContext;
using BNS.Resource;
using BNS.Domain.Responses;
using BNS.Domain;
using Microsoft.Extensions.Localization;
using BNS.Data.Entities.JM_Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BNS.Service.Features
{
    public class GetProjectByUserIdQuery : GetRequestHandler<ProjectResponseItem, JM_Project>
    {
        protected readonly BNSDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public GetProjectByUserIdQuery(
           IStringLocalizer<SharedResource> sharedLocalizer,
           IMapper mapper,
           IUnitOfWork unitOfWork) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
        }

        public override IQueryable<JM_Project> GetQueryableData(CommandGetRequest<ApiResultList<ProjectResponseItem>> request)
        {
            var query = _unitOfWork.Repository<JM_Project>()
                .Include(s => s.Sprints)
                .Where(s => !s.IsDelete && s.CompanyId == request.CompanyId)
                .OrderByDescending(d => d.CreatedDate).AsQueryable();

            return query;
        }
    }
}
