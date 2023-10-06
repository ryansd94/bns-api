using BNS.Domain;
using BNS.Resource;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Threading;
using System.Threading.Tasks;
using BNS.Domain.Commands;
using BNS.Data.Entities.JM_Entities;
using AutoMapper;
using BNS.Domain.Interface;

namespace BNS.Service.Features
{
    public class CreateViewPermissionCommand : IRequestHandler<CreateViewPermissionRequest, ApiResult<Guid>>
    {
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        protected readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccountService _accountService;

        public CreateViewPermissionCommand(
            IStringLocalizer<SharedResource> sharedLocalizer,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IAccountService accountService)
        {
            _sharedLocalizer = sharedLocalizer;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _accountService = accountService;
        }

        public async Task<ApiResult<Guid>> Handle(CreateViewPermissionRequest request, CancellationToken cancellationToken)
        {
            var viewPermission = _mapper.Map<SYS_ViewPermission>(request);
            _unitOfWork.Repository<SYS_ViewPermission>().Add(viewPermission);
            foreach (var item in request.Permission)
            {
                var viewPermissionAction = _mapper.Map<SYS_ViewPermissionAction>(item);
                viewPermissionAction.ViewPermissionId = viewPermission.Id;
                viewPermissionAction.CreatedUserId = request.UserId;
                viewPermissionAction.CompanyId = request.CompanyId;
                _unitOfWork.Repository<SYS_ViewPermissionAction>().Add(viewPermissionAction);
                foreach (var action in item.Actions)
                {
                    if (action.Value == false)
                        continue;
                    var viewPermissionActionDetail = _mapper.Map<SYS_ViewPermissionActionDetail>(action);
                    viewPermissionActionDetail.ViewPermissionActionId = viewPermissionAction.Id;
                    viewPermissionActionDetail.CreatedUserId = request.UserId;
                    viewPermissionActionDetail.CompanyId = request.CompanyId;
                    _unitOfWork.Repository<SYS_ViewPermissionActionDetail>().Add(viewPermissionActionDetail);
                }
            }
            if (request.UserSelectedIds != null)
            {
                foreach (var userId in request.UserSelectedIds)
                {
                    _unitOfWork.Repository<SYS_ViewPermissionObject>().Add(new SYS_ViewPermissionObject
                    {
                        ViewPermissionId = viewPermission.Id,
                        ObjectType = Utilities.Enums.EPermissionObject.User,
                        ObjectId = userId,
                        CreatedUserId = request.UserId,
                        CompanyId = request.CompanyId
                    });
                }
            }
            if (request.TeamSelectedIds != null)
            {
                foreach (var teamId in request.TeamSelectedIds)
                {
                    _unitOfWork.Repository<SYS_ViewPermissionObject>().Add(new SYS_ViewPermissionObject
                    {
                        ViewPermissionId = viewPermission.Id,
                        ObjectType = Utilities.Enums.EPermissionObject.Team,
                        ObjectId = teamId,
                        CreatedUserId = request.UserId,
                        CompanyId = request.CompanyId
                    });
                }
            }
            var response = await _unitOfWork.SaveChangesAsync();
            _accountService.UpdateUserPermission(request.UserSelectedIds, request.TeamSelectedIds);
            return response;
        }
    }
}
