﻿using AutoMapper;
using BNS.Data.EntityContext;
using BNS.Resource;
using BNS.Domain.Responses;
using BNS.Domain;
using Microsoft.Extensions.Localization;
using BNS.Data.Entities.JM_Entities;

namespace BNS.Service.Features
{
    public class GetProjectByUserIdQuery : GetRequestHandler<ProjectResponseItem, JM_Project>
    {
        protected readonly BNSDbContext _context;

        public GetProjectByUserIdQuery(
           IStringLocalizer<SharedResource> sharedLocalizer,
           IMapper mapper,
           IUnitOfWork unitOfWork) : base(unitOfWork, mapper)
        {
        }
    }
}