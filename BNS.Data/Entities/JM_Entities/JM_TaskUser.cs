using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BNS.Data.Entities.JM_Entities
{
    public class JM_TaskUser
    {
        [Key]
        public Guid Id { get; set; }
        public Guid TaskId { get; set; }
        public Guid UserId { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatedUserId { get; set; }
        public Guid? CompanyId { get; set; }
        public bool IsDelete { get; set; }
        [ForeignKey("TaskId")]
        public virtual JM_Task Task { get; set; }
        [ForeignKey("UserId")]
        public virtual JM_Account User { get; set; }
    }
}
