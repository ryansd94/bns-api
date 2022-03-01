
using AutoMapper;
using BNS.Data.Entities.JM_Entities;
using BNS.Data.EntityContext;
using BNS.Domain;
using BNS.Resource;
using BNS.Utilities;
using BNS.ViewModels;
using BNS.ViewModels.Responses.Project;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;


namespace BNS.Application.Features
{
    public class GetJM_SprintQuery
    {
        public class GetJM_SprintRequest : CommandRequest<ApiResult<JM_SprintResponse>>
        {
            [Required]
            public Guid JM_ProjectId { get; set; }
        }
        public class GetJM_SprintRequestHandler : IRequestHandler<GetJM_SprintRequest, ApiResult<JM_SprintResponse>>
        {
            protected readonly BNSDbContext _context;
            protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public GetJM_SprintRequestHandler(BNSDbContext context,
             IStringLocalizer<SharedResource> sharedLocalizer,
             IUnitOfWork unitOfWork,
                IMapper mapper)
            {
                _context = context;
                _sharedLocalizer = sharedLocalizer;
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<ApiResult<JM_SprintResponse>> Handle(GetJM_SprintRequest request, CancellationToken cancellationToken)
            {

                var response = new ApiResult<JM_SprintResponse>();
                response.data = new JM_SprintResponse();
                Expression<Func<JM_Sprint, bool>> filter = s => !s.IsDelete && s.CompanyIndex == request.CompanyId;

                var query = (await _unitOfWork.JM_SprintRepository.GetAsync(filter,
                    s => s.OrderBy(d => d.Name))).Select(s => _mapper.Map<JM_SprintResponseItem>(s));

                if (!string.IsNullOrEmpty(request.fieldSort))
                    query = Common.OrderBy(query, request.fieldSort, request.sort == ESortEnum.desc.ToString() ? false : true);

                response.recordsTotal = await _unitOfWork.JM_SprintRepository.CountAsync(filter);
                query = query.Skip(request.start).Take(request.length);
                var rs = await query.ToListAsync();
                response.data.Items = rs;
                return response;

            }

        }
    }
}
