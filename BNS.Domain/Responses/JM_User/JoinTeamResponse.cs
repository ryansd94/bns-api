using System;
namespace BNS.Domain.Responses
{
    public class JoinTeamResponse
    {
        public string Key { get; set; }
        public string EmailJoin { get; set; }
        public Guid CompanyId { get; set; }
        public Guid  UserRequest { get; set; }
        public Guid Id { get; set; }
    }
}
