using System;
namespace BNS.Domain.Responses
{
    public class LoginResponse
    {
        public string UserName { get; set; }
        public string Image { get; set; }
        public string FullName { get; set; }
        public string ShopCode { get; set; }
        public SettingResponse Setting { get; set; }
        public Guid ShopIndex { get; set; }
        public string Token { get; set; }
        public string UserId { get; set; }
        public bool MainAccount { get; set; }
        public string DefaultOrganization { get; set; }

    }
}
