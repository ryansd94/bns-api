﻿using AutoMapper;
using BNS.Data.Entities.JM_Entities;
using BNS.Domain;
using BNS.Domain.Commands;
using BNS.Resource;
using BNS.Service.Implement.BaseImplement;
using Microsoft.Extensions.Localization;

namespace BNS.Service.Features
{
    public class UpdateStatusCommand : UpdateRequestHandler<UpdateStatusRequest, JM_Status>
    {
        public UpdateStatusCommand(IUnitOfWork unitOfWork,
            IMapper mapper) : base(unitOfWork, mapper)
        {
        }
    }
}
