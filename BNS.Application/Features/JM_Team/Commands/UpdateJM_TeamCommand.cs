using BNS.Data.Entities.JM_Entities;
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
            var dataCheck = await _unitOfWork.JM_TeamRepository.FirstOrDefaultAsync(s => s.Id == request.Id &&
            s.CompanyId == request.CompanyId);
            if (dataCheck == null)
            {
                response.errorCode = EErrorCode.NotExistsData.ToString();
                response.title = _sharedLocalizer[LocalizedBackendMessages.MSG_NotExistsData];
                return response;
            }

            var checkDuplicate = await _unitOfWork.JM_TeamRepository.FirstOrDefaultAsync(s => s.Name.Equals(request.Name)
            && s.Id != request.Id
            && s.CompanyId == request.CompanyId);
            if (checkDuplicate != null)
            {
                response.errorCode = EErrorCode.IsExistsData.ToString();
                response.title = _sharedLocalizer[LocalizedBackendMessages.MSG_ExistsData];
                return response;
            }
            if (request.Members ==null || request.Members.Count ==0)
            {
                var teamMembers = await (await _unitOfWork.JM_TeamMemberRepository.GetAsync(s => s.TeamId==request.Id && !s.IsDelete)).ToListAsync();
                foreach (var item in teamMembers)
                {
                    item.IsDelete=true;
                    item.UpdatedDate=DateTime.UtcNow;
                    item.UpdatedUser=request.UserId;
                    await _unitOfWork.JM_TeamMemberRepository.UpdateAsync(item);
                }
            }
            else
            {
                var userContain = await _unitOfWork.JM_AccountCompanyRepository.GetAsync(s => request.Members.Contains(s.UserId));
                var memberAdd = request.Members.Where(s => userContain.Select(d => d.UserId).Contains(s)).ToList();
                var teamMembers = await (await _unitOfWork.JM_TeamMemberRepository.GetAsync(s => s.TeamId==request.Id)).ToListAsync();

                if (teamMembers == null || teamMembers.Count ==0)
                {
                    foreach (var item in memberAdd)
                    {
                        await _unitOfWork.JM_TeamMemberRepository.AddAsync(new JM_TeamMember
                        {
                            CompanyId=request.CompanyId,
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
                    var teamMemberDelete = teamMembers.Where(s => !memberAdd.Contains(s.UserId) && !s.IsDelete).ToList();
                    foreach (var item in teamMemberDelete)
                    {
                        item.IsDelete=true;
                        item.UpdatedDate=DateTime.UtcNow;
                        item.UpdatedUser=request.UserId;
                        await _unitOfWork.JM_TeamMemberRepository.UpdateAsync(item);
                    }
                    var teamMemberUpdate = teamMembers.Where(s => memberAdd.Contains(s.UserId) &&  s.IsDelete).ToList();
                    foreach (var item in teamMemberUpdate)
                    {
                        item.IsDelete=false;
                        item.UpdatedDate=DateTime.UtcNow;
                        item.UpdatedUser=request.UserId;
                        await _unitOfWork.JM_TeamMemberRepository.UpdateAsync(item);
                    }
                    var teamMemberAdd = memberAdd.Where(d => !teamMembers.Select(s => s.UserId).Contains(d));
                    foreach (var item in teamMemberAdd)
                    {
                        await _unitOfWork.JM_TeamMemberRepository.AddAsync(new JM_TeamMember
                        {
                            CompanyId=request.CompanyId,
                            CreatedDate=DateTime.UtcNow,
                            CreatedUser=request.UserId,
                            Id=Guid.NewGuid(),
                            IsDelete=false,
                            TeamId=request.Id,
                            UserId=item
                        });
                    }
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
