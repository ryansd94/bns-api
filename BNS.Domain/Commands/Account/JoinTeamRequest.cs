using BNS.Domain.Responses;
using System;

namespace BNS.Domain.Commands.Account
{
    public class JoinTeamRequest: CommandBase<ApiResult<LoginResponse>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [PasswordValidationAttribute(true)]
        public string Password { get; set; }
        public string Image { get; set; }
        public Guid AccountCompanyId { get; set; }
        public bool IsHasAccount { get; set; }
    }
}
