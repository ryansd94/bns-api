using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BNS.Data.Entities.JM_Entities
{
    public class JM_Account : IdentityUser<Guid>
    {
        [MaxLength(200)]
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatedUser { get; set; }
        public bool IsDelete { get; set; }
        public string GoogleId { get; set; }
        public bool IsActive { get; set; }
        public string Image { get; set; }
        public Guid? JM_CompanyId { get; set; }
        public Guid? JM_TaskId { get; set; }
        public string Setting { get; set; }
        public virtual ICollection<JM_AccountCompany> AccountCompanys { get; set; }
    }
}
