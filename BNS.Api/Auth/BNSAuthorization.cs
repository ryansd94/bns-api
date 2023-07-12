using BNS.Domain;
using BNS.Domain.Commands;
using BNS.Domain.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using static BNS.Utilities.Enums;

namespace BNS.Api.Auth
{
    public class BNSAuthorization : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly bool _isCheckPermission;
        public BNSAuthorization(bool isCheckPermission = true)
        {
            _isCheckPermission = isCheckPermission;
        }
        public async void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                var organizationRoute = context.RouteData.Values["organization"];
                var method = context.HttpContext.Request.Method;
                var accountCompanyId = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == EClaimType.AccountCompanyId.ToString());
                var companyId = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == EClaimType.CompanyId.ToString());
                var userId = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == EClaimType.UserId.ToString());
                var organization = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == EClaimType.Organization.ToString());
                var strRole = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == EClaimType.Role.ToString())?.Value;
                var action = context.HttpContext.Request.RouteValues["action"].ToString();
                var controller = context.HttpContext.Request.RouteValues["controller"].ToString();
                var isMainAccount = bool.Parse(context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == EClaimType.IsMainAccount.ToString())?.Value);
                #region validate request
                if (organization == null || string.IsNullOrEmpty(organization.Value))
                {
                    context.Result = new ApiResult<string> { status = System.Net.HttpStatusCode.Unauthorized, errorCode = EErrorCode.NotPermission.ToString() };
                    return;
                }
                var organizationObjects = JsonConvert.DeserializeObject<List<string>>(organization.Value);
                if (!organizationObjects.Contains(organizationRoute))
                {
                    context.Result = new ApiResult<string> { status = System.Net.HttpStatusCode.Unauthorized, errorCode = EErrorCode.NotPermission.ToString() };
                    return;
                }
                if (string.IsNullOrEmpty(strRole))
                {
                    context.Result = new ApiResult<string> { status = System.Net.HttpStatusCode.Unauthorized, errorCode = EErrorCode.NotPermission.ToString() };
                    return;
                }
                #endregion

                if (!isMainAccount && _isCheckPermission == true)
                {
                    var permissions = JsonConvert.DeserializeObject<List<ViewPermissionAction>>(strRole);
                    var accountService = context.HttpContext.RequestServices.GetService<IAccountService>();
                    var isPermission = await accountService.CheckPermissionForUser(false, controller, action, Enum.Parse<ERestMethod>(method), Guid.Parse(accountCompanyId.Value));
                    if (!isPermission)
                    {
                        context.Result = new ApiResult<string> { status = System.Net.HttpStatusCode.Unauthorized, errorCode = EErrorCode.NotPermission.ToString() };
                        return;
                    }
                }
                if (method == "GET")
                {
                    var query = context.HttpContext.Request.QueryString;
                    query = query.Add("CompanyId", companyId.Value);
                    query = query.Add("UserId", userId.Value);
                    query = query.Add("isMainAccount", isMainAccount.ToString());
                    context.HttpContext.Request.QueryString = query;
                }
                else if (method == "POST" || method == "PUT")
                {
                    var bodyStr = "";
                    var req = context.HttpContext.Request;
                    // Allows using several time the stream in ASP.Net Core
                    req.EnableBuffering();

                    // Arguments: Stream, Encoding, detect encoding, buffer size 
                    // AND, the most important: keep stream opened

                    using (StreamReader reader
                              = new StreamReader(req.Body, Encoding.UTF8, true, 2048, true))
                    {
                        var bodyStr2 = reader.ReadToEndAsync();
                        bodyStr = bodyStr2.Result;
                    }

                    Dictionary<string, object> data = null;
                    if (!string.IsNullOrEmpty(bodyStr))
                        data = JsonConvert.DeserializeObject<Dictionary<string, object>>(bodyStr);
                    else data = new Dictionary<string, object>();
                    data.Add("CompanyId", companyId.Value);
                    data.Add("UserId", userId.Value);
                    var requestData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data));
                    context.HttpContext.Request.Body = new MemoryStream(requestData);
                }
                return;
            }
            catch (System.Exception)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
        }
    }
}
