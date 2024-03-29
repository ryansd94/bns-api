﻿using BNS.Data.Entities.JM_Entities;
using BNS.Domain;
using BNS.Domain.Commands;
using BNS.Resource;
using BNS.Resource.LocalizationResources;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Threading;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;

namespace BNS.Service.Features
{
    public class CreateTaskTypeCommand : IRequestHandler<CreateTaskTypeRequest, ApiResult<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public CreateTaskTypeCommand(
         IStringLocalizer<SharedResource> sharedLocalizer,
         IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _sharedLocalizer = sharedLocalizer;
        }
        public async Task<ApiResult<Guid>> Handle(CreateTaskTypeRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<Guid>();
            var dataCheck = await _unitOfWork.Repository<JM_TaskType>().FirstOrDefaultAsync(s => s.Name.Equals(request.Name) &&
            s.CompanyId == request.CompanyId && !s.IsDelete);
            if (dataCheck != null)
            {
                response.errorCode = EErrorCode.IsExistsData.ToString();
                response.title = _sharedLocalizer[LocalizedBackendMessages.MSG_ExistsData];
                return response;
            }
            var data = new JM_TaskType
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                Icon = request.Icon,
                CreatedDate = DateTime.UtcNow,
                CreatedUserId = request.UserId,
                Order = request.Order,
                CompanyId = request.CompanyId,
                Color = request.Color,
                ColorFilter = request.ColorFilter,
                TemplateId = request.TemplateId,
            };
            await _unitOfWork.Repository<JM_TaskType>().AddAsync(data);
            await _unitOfWork.SaveChangesAsync();
            response.data = data.Id;
            return response;
        }

    }
}
