using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BNS.Data.Entities.JM_Entities
{
    public partial class JM_ProjectMember : BaseJMEntity
    {
        public Guid ProjectId { get; set; }
        public Guid UserId { get; set; }
        public bool IsCreated { get; set; }
        [ForeignKey("UserId")]
        public virtual JM_Account JM_Account { get; set; }
        [ForeignKey("ProjectId")]
        public virtual JM_Project JM_Project { get; set; }
    }
}
