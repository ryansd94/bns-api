using AutoMapper;
using BNS.Data.Entities.JM_Entities;
using BNS.Domain;
using BNS.Domain.Queries;
using BNS.Domain.Responses;
using BNS.Resource;
using BNS.Utilities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BNS.Service.Features
{
    public class GetTaskQuery : IRequestHandler<GetTaskRequest, ApiResult<TaskResponse>>
    {
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetTaskQuery(
            IStringLocalizer<SharedResource> sharedLocalizer,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _sharedLocalizer = sharedLocalizer;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResult<TaskResponse>> Handle(GetTaskRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<TaskResponse>();
            response.data = new TaskResponse();

            var query1 = _unitOfWork.Repository<JM_Task>().Where(s => !s.IsDelete &&
              s.CompanyId == request.CompanyId && s.ReporterId == request.UserId)
                .OrderBy(d => d.CreatedDate).Select(s => s.Id);

            var query2 = _unitOfWork.Repository<JM_TaskUser>().Where(s => !s.IsDelete &&
               s.CompanyId == request.CompanyId && s.UserId == request.UserId)
                .OrderBy(d => d.CreatedDate).Select(s => s.TaskId);

            var query3 = await query1.Union(query2).Distinct().ToListAsync();

            var query = _unitOfWork.Repository<JM_Task>()
                .Include(s => s.TaskType)
                .Include(s => s.Status)
                .Include(s => s.User)
                .Where(s => query3.Contains(s.Id))
                .OrderBy(d => d.CreatedDate).Select(s => new TaskItem
                {
                    Id = s.Id,
                    Title = s.Title,
                    Icon = s.TaskType.Icon,
                    TaskTypeName = s.TaskType.Name,
                    Status = new StatusResponseItem
                    {
                        Name = s.Status != null ? s.Status.Name : "",
                        Color = s.Status != null ? s.Status.Color : "",
                    },
                    CreatedDate = s.CreatedDate,
                    CreatedUserId = s.CreatedUserId,
                    CreateUser = new TaskUser
                    {
                        Name = s.User != null ? s.User.FullName : "",
                        Image = s.User != null ? s.User.Image : "",
                    }
                });

            if (!string.IsNullOrEmpty(request.fieldSort))
            {
                var columnSort = request.fieldSort;
                var sortType = request.sort;
                if (!string.IsNullOrEmpty(columnSort) && !request.isAdd && !request.isEdit)
                {
                    columnSort = columnSort[0].ToString().ToUpper() + columnSort.Substring(1, columnSort.Length - 1);
                    query = query.OrderBy(request.fieldSort, request.sort);
                }
            }

            if (!string.IsNullOrEmpty(request.fieldSort))
            {
                query = query.OrderBy(request.fieldSort, request.sort);
            }

            query = query.WhereOr(request.filters);
            response.recordsTotal = await query.CountAsync();
            if (!request.isGetAll)
                query = query.Skip(request.start).Take(request.length);

            var rs = await query.ToListAsync();

            response.data.Items = rs;
            return response;
        }

    }
}
