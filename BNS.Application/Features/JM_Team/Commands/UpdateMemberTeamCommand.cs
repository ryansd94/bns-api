﻿using BNS.Data.Entities.JM_Entities;
using BNS.Domain;
using BNS.Resource;
using BNS.Resource.LocalizationResources;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;
using BNS.Domain.Commands;
namespace BNS.Service.Features
{
    public class UpdateMemberTeamCommand : IRequestHandler<UpdateMemberTeamRequest, ApiResult<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public UpdateMemberTeamCommand(IUnitOfWork unitOfWork,
         IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _unitOfWork = unitOfWork;
            _sharedLocalizer = sharedLocalizer;
        }
        public async Task<ApiResult<Guid>> Handle(UpdateMemberTeamRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<Guid>();
            var team = await _unitOfWork.JM_TeamRepository.FirstOrDefaultAsync(s => s.Id == request.Id &&
            s.CompanyId == request.CompanyId);
            if (team == null)
            {
                response.errorCode = EErrorCode.NotExistsData.ToString();
                response.title = _sharedLocalizer[LocalizedBackendMessages.MSG_NotExistsData];
                return response;
            }
            if (request.Members != null && request.Members.Count >0)
            {
                var userContain = await _unitOfWork.JM_AccountCompanyRepository.GetAsync(s => request.Members.Contains(s.UserId));
                var memberAdd = request.Members.Where(s => userContain.Select(d => d.UserId).Contains(s)).ToList();
                
                team.UpdatedDate = DateTime.UtcNow;
                team.UpdatedUserId = request.UserId;

                await _unitOfWork.JM_TeamRepository.UpdateAsync(team);
                response =await _unitOfWork.SaveChangesAsync();
            }
            response.data = team.Id;
            return response;
        }
    }
}
