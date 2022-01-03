using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

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
        public Guid ShopIndex
        {
            get
            {
                var shopIndex = _caller.Claims.Single(c => c.Type == "ShopIndex");
                return shopIndex != null ? new Guid(shopIndex.Value) : Guid.Empty;
            }
        }
    }
}
