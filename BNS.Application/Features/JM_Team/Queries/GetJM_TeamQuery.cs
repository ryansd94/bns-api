
using AutoMapper;
using BNS.Data.Entities.JM_Entities;
using BNS.Data.EntityContext;
using BNS.Domain;
using BNS.Resource;
using BNS.Utilities;
using BNS.Domain.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Nest;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BNS.Domain.Queries;

namespace BNS.Service.Features
{
    public class GetJM_TeamQuery : IRequestHandler<GetJM_TeamRequest, ApiResult<TeamResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetJM_TeamQuery(
         IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResult<TeamResponse>> Handle(GetJM_TeamRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<TeamResponse>();
            response.data = new TeamResponse();

            var query = _unitOfWork.Repository<JM_Team>().Where(s => !s.IsDelete
               && s.CompanyId == request.CompanyId).OrderBy(d => d.CreatedDate).Select(s => new TeamResponseItem
               {
                   Name = s.Name,
                   Description = s.Description,
                   Id = s.Id,
                   CreatedDate = s.CreatedDate,
                   ParentId = s.ParentId,
                   TeamMembers = s.JM_AccountCompanys.Select(u => u.Id).ToList(),
                   ParentName = s.TeamParent != null && !s.TeamParent.IsDelete ? s.TeamParent.Name : String.Empty
               });

            if (!string.IsNullOrEmpty(request.fieldSort))
            {
                query = query.OrderBy(request.fieldSort, request.sort);
            }
            if (!string.IsNullOrEmpty(request.filters))
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
