using BNS.Domain.Commands;
using System;
using System.Collections.Generic;

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
        public string AccountCompanyId { get; set; }
        public bool IsMainAccount { get; set; }
        public CompanyResponse DefaultOrganization { get; set; }
        public List<ViewPermissionAction> ViewPermissions { get; set; }
        public List<ProjectUserResponse> Projects { get; set; }
    }

    public class ProjectUserResponse
    {
        public string Name { get; set; }
        public Guid Id { get; set; }
        public string Code { get; set; }
    }
}
