using AutoMapper;
using BNS.Data.EntityContext;
using BNS.Resource;
using BNS.Domain.Responses;
using BNS.Domain;
using Microsoft.Extensions.Localization;
using BNS.Data.Entities.JM_Entities;

namespace BNS.Service.Features
{
    public class GetProjectByUserIdQuery : GetRequestHandler<ProjectResponseItem, JM_Project>
    {
        protected readonly BNSDbContext _context;
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetProjectByUserIdQuery(
           IStringLocalizer<SharedResource> sharedLocalizer,
           IMapper mapper,
           IUnitOfWork unitOfWork) : base(unitOfWork, mapper)
        {
            _sharedLocalizer = sharedLocalizer;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        //public async Task<ApiResult<ProjectResponse>> Handle(GetJM_ProjectByUserIdRequest request, CancellationToken cancellationToken)
        //{
        //    var response = new ApiResult<ProjectResponse>();
        //    response.data = new ProjectResponse();

        //    var query = _unitOfWork.Repository<JM_Project>().Where(s => !s.IsDelete
        //       && s.CompanyId == request.CompanyId).OrderBy(d => d.CreatedDate).Select(s => _mapper.Map<ProjectResponseItem>(s));

        //    if (!string.IsNullOrEmpty(request.fieldSort))
        //    {
        //        query = query.OrderBy(request.fieldSort, request.sort);
        //    }
        //    query = query.WhereOr(request.filters);
        //    response.recordsTotal = await query.CountAsync();
        //    if (!request.isGetAll)
        //        query = query.Skip(request.start).Take(request.length);

        //    var rs = await query.ToListAsync();
        //    response.data.Items = rs;
        //    return response;
        //}

    }

}
