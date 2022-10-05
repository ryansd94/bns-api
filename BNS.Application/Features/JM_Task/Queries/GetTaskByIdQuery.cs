
using AutoMapper;
using BNS.Data.EntityContext;
using BNS.Resource;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BNS.Domain.Queries;
using BNS.Domain.Responses;
using BNS.Domain;

namespace BNS.Service.Features
{
    public class GetTaskByIdQuery : IRequestHandler<GetJM_TaskByIdRequest, ApiResult<TaskItem>>
    {
        protected readonly BNSDbContext _context;
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IMapper _mapper;

        public GetTaskByIdQuery(BNSDbContext context,
         IStringLocalizer<SharedResource> sharedLocalizer,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _sharedLocalizer = sharedLocalizer;
        }
        public async Task<ApiResult<TaskItem>> Handle(GetJM_TaskByIdRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<TaskItem>();
            var query = _context.JM_Tasks.Where(s => s.Id == request.Id &&
            !s.IsDelete).Select(s => _mapper.Map<TaskItem>(s));
            var rs = await query.FirstOrDefaultAsync();
            response.data = rs;
            return response;
        }

    }
}
