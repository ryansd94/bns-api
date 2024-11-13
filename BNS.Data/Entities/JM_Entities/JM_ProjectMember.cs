using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BNS.Data.Entities.JM_Entities
{
    public partial class JM_ProjectMember : BaseJMEntity
    {
        public Guid ProjectId { get; set; }
        public Guid AccountCompanyId { get; set; }
        public bool IsCreated { get; set; }
        [ForeignKey("AccountCompanyId")]
        public virtual JM_AccountCompany AccountCompany { get; set; }
        [ForeignKey("ProjectId")]
        public virtual JM_Project Project { get; set; }
    }
}
