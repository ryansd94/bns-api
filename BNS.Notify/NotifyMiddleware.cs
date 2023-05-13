using BNS.Domain;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using static BNS.Utilities.Enums;

namespace BNS.Notify
{
    public class NotifyMiddleware
    {
        private readonly RequestDelegate _next;
        protected readonly MyConfiguration _config;

        public NotifyMiddleware(RequestDelegate next, IOptions<MyConfiguration> config)
        {
            _next = next;
            _config = config.Value;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var request = httpContext.Request;
            if (request.Path.StartsWithSegments("/notify", StringComparison.OrdinalIgnoreCase))
            {
                var accessToken = request.Query["access_token"];
                var jwtToken = request.Headers["Authorization"];
                if (!string.IsNullOrWhiteSpace(jwtToken))
                {
                    var token = jwtToken;
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = System.Text.Encoding.UTF8.GetBytes(_config.Tokens.Key);
                    var issuer = _config.Tokens.Issuer;
                    try
                    {
                        tokenHandler.ValidateToken(token, new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(key),
                            ValidateIssuer = true,
                            ValidIssuer = issuer,
                            ValidAudience = issuer,
                            ValidateLifetime = true,
                        }, out SecurityToken validatedToken);
                        httpContext.Request.Headers["Authorization"] = "Bearer " + token;
                        var tokenReader = new JwtSecurityTokenHandler().ReadJwtToken(token);
                        var accountCompanyId = tokenReader.Claims.First(c => c.Type == EClaimType.AccountCompanyId.ToString())?.Value;
                        var claims = new[]
                        {
                            new Claim(EClaimType.AccountCompanyId.ToString(), accountCompanyId != null ? accountCompanyId.ToString() : string.Empty),
                        };
                        var x = ConvertToEnumerable(new ClaimsIdentity(claims));
                        httpContext.User.AddIdentities(x);
                    }
                    catch
                    {
                        httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        return;
                    }
                }
            }
            await _next(httpContext);
        }
        public IEnumerable<ClaimsIdentity> ConvertToEnumerable(ClaimsIdentity identity)
        {
            yield return identity;
        }
    }
}
