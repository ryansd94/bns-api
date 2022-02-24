using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BNS.Api.Auth
{
    public class BNSAuthorization : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                var method = context.HttpContext.Request.Method;
                var companyId = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "CompanyId");
                var userId = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId");
                if (method == "GET")
                {
                    var query = context.HttpContext.Request.QueryString;
                    query= query.Add("CompanyId", companyId.Value);
                    query= query.Add("UserId", userId.Value);
                    context.HttpContext.Request.QueryString=query;
                }
                else if (method == "POST" ||    method=="PUT")
                {
                    var bodyStr = "";
                    var req = context.HttpContext.Request;
                    // Allows using several time the stream in ASP.Net Core
                    req.EnableBuffering();

                    // Arguments: Stream, Encoding, detect encoding, buffer size 
                    // AND, the most important: keep stream opened
                    using (StreamReader reader
                              = new StreamReader(req.Body, Encoding.UTF8, true, 1024, true))
                    {
                        bodyStr = reader.ReadToEnd();
                    }
                    var data = JsonConvert.DeserializeObject<Dictionary<string, object>>(bodyStr);
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
