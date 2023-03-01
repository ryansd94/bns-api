
using AutoMapper;
using BNS.Data.Entities.JM_Entities;
using BNS.Data.EntityContext;
using BNS.Domain;
using BNS.Resource;
using BNS.Domain.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BNS.Domain.Queries;

namespace BNS.Service.Features
{
    public class GetChildrenCommentQuery : IRequestHandler<GetCommentRequest, ApiResult<CommentResponse>>
    {
        protected readonly BNSDbContext _context;
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetChildrenCommentQuery(BNSDbContext context,
            IStringLocalizer<SharedResource> sharedLocalizer,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _context = context;
            _mapper = mapper;
            _sharedLocalizer = sharedLocalizer;
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResult<CommentResponse>> Handle(GetCommentRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<CommentResponse>();
            response.data = new CommentResponse();

            var query = _unitOfWork.Repository<JM_Comment>().Include(s => s.User).Where(s => s.CompanyId == request.CompanyId && s.ParentId == request.ParentId).OrderByDescending(d => d.CreatedDate).Select(s => _mapper.Map<CommentResponseItem>(s));

            response.recordsTotal = await query.CountAsync();
            if (!request.isGetAll)
                query = query.Skip(request.start).Take(request.length);

            var rs = await query.ToListAsync();
            response.data.Items = rs;
            return response;
        }
    }
}
