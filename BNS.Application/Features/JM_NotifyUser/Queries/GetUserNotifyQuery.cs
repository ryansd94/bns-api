using AutoMapper;
using BNS.Data.Entities.JM_Entities;
using BNS.Data.EntityContext;
using BNS.Domain;
using BNS.Domain.Queries;
using BNS.Domain.Responses;
using BNS.Utilities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BNS.Service.Features
{
    public class GetUserNotifyQuery : IRequestHandler<GetNotifyUserRequest, ApiResult<NotifyUserResponse>>
    {
        protected readonly BNSDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetUserNotifyQuery(BNSDbContext context,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _context = context;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResult<NotifyUserResponse>> Handle(GetNotifyUserRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<NotifyUserResponse>();
            response.data = new NotifyUserResponse();
            var unRead = await _unitOfWork.Repository<JM_NotifycationUser>()
                .AsNoTracking()
                .Where(s => !s.IsDelete
                    && s.CompanyId == request.CompanyId
                    && s.IsRead == false
                    && s.UserReceivedId == request.UserId)
                .CountAsync();
            var query = _unitOfWork.Repository<JM_NotifycationUser>()
                .AsNoTracking()
                .Where(s => !s.IsDelete
                    && (request.IsRead == null || (request.IsRead != null && s.IsRead == request.IsRead.Value))
                    && s.CompanyId == request.CompanyId
                    && s.UserReceivedId == request.UserId)
                .OrderByDescending(d => d.CreatedDate)
                .Select(s => _mapper.Map<NotifyResponse>(s));

            if (!string.IsNullOrEmpty(request.fieldSort))
            {
                query = query.OrderBy(request.fieldSort, request.sort);
            }
            query = query.WhereOr(request.filters);
            response.recordsTotal = await query.CountAsync();
            response.data.Unread = unRead;
            if (!request.isGetAll)
                query = query.Skip(request.start).Take(request.length);

            var rs = await query.ToListAsync();
            response.data.Items = rs;
            return response;
        }
    }
}
