using BNS.Data.Entities.JM_Entities;
using BNS.Domain;
using BNS.Resource;
using BNS.Resource.LocalizationResources;
using BNS.Models;
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
    public class UpdateJM_TeamCommand : IRequestHandler<UpdateJM_TeamRequest, ApiResult<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public UpdateJM_TeamCommand(IUnitOfWork unitOfWork,
         IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _unitOfWork = unitOfWork;
            _sharedLocalizer = sharedLocalizer;
        }
        public async Task<ApiResult<Guid>> Handle(UpdateJM_TeamRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<Guid>();
            var dataCheck = await _unitOfWork.JM_TeamRepository.GetDefaultAsync(s => s.Id == request.Id);
            if (dataCheck == null)
            {
                response.errorCode = EErrorCode.NotExistsData.ToString();
                response.title = _sharedLocalizer[LocalizedBackendMessages.MSG_NotExistsData];
                return response;
            }

            var checkDuplicate = await _unitOfWork.JM_TeamRepository.GetDefaultAsync(s => s.Name.Equals(request.Name)
            && s.Id != request.Id
            && s.CompanyIndex == request.CompanyId);
            if (checkDuplicate != null)
            {
                response.errorCode = EErrorCode.IsExistsData.ToString();
                response.title = _sharedLocalizer[LocalizedBackendMessages.MSG_ExistsData];
                return response;
            }

            var teamMembers = await (await _unitOfWork.JM_TeamMemberRepository.GetAsync(s => s.TeamId==request.Id)).ToListAsync();

            if (teamMembers == null || teamMembers.Count ==0)
            {
                foreach (var item in request.Members)
                {
                    await _unitOfWork.JM_TeamMemberRepository.AddAsync(new JM_TeamMember
                    {
                        CompanyIndex=request.CompanyId,
                        CreatedDate=DateTime.UtcNow,
                        CreatedUser=request.UserId,
                        Id=Guid.NewGuid(),
                        IsDelete=false,
                        TeamId=request.Id,
                        UserId=item
                    });
                }
            }
            else
            {
                var teamMemberDelete = teamMembers.Where(s => !request.Members.Contains(s.TeamId) && !s.IsDelete).ToList();
                foreach (var item in teamMemberDelete)
                {
                    item.IsDelete=true;
                    item.UpdatedDate=DateTime.UtcNow;
                    item.UpdatedUser=request.UserId;
                    await _unitOfWork.JM_TeamMemberRepository.UpdateAsync(item);
                }
                var teamMemberUpdate = teamMembers.Where(s => request.Members.Contains(s.TeamId) &&  s.IsDelete).ToList();
                foreach (var item in teamMemberUpdate)
                {
                    item.IsDelete=false;
                    item.UpdatedDate=DateTime.UtcNow;
                    item.UpdatedUser=request.UserId;
                    await _unitOfWork.JM_TeamMemberRepository.UpdateAsync(item);
                }
                var teamMemberAdd = request.Members.Where(d => !teamMembers.Select(s => s.TeamId).Contains(d));
                foreach (var item in teamMemberAdd)
                {
                    await _unitOfWork.JM_TeamMemberRepository.AddAsync(new JM_TeamMember
                    {
                        CompanyIndex=request.CompanyId,
                        CreatedDate=DateTime.UtcNow,
                        CreatedUser=request.UserId,
                        Id=Guid.NewGuid(),
                        IsDelete=false,
                        TeamId=request.Id,
                        UserId=item
                    });
                }
            }
            dataCheck.Code = request.Code;
            dataCheck.Name = request.Name;
            dataCheck.Description = request.Description;
            dataCheck.ParentId = request.ParentId;
            dataCheck.UpdatedDate = DateTime.UtcNow;
            dataCheck.UpdatedUser = request.UserId;

            await _unitOfWork.JM_TeamRepository.UpdateAsync(dataCheck);
            response =await _unitOfWork.SaveChangesAsync();
            response.data = dataCheck.Id;
            return response;
        }

    }
}
