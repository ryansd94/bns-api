﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;

namespace BNS.Api
{
    public class BaseController : ControllerBase
    {
        private readonly ClaimsPrincipal _caller;

        public BaseController(IHttpContextAccessor httpContextAccessor)
        {
            _caller = httpContextAccessor.HttpContext.User;
        }

        public Guid UserId
        {
            get
            {
                var userId = _caller.Claims.Single(c => c.Type == "UserId");
                return userId != null ? new Guid(userId.Value) : Guid.Empty;
            }
        }
        public Guid CompanyId
        {
            get
            {
                var shopIndex = _caller.Claims.Single(c => c.Type == "CompanyId");
                return shopIndex != null ? new Guid(shopIndex.Value) : Guid.Empty;
            }
        }
    }
}
