using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BNS.Data.Entities.JM_Entities
{
    public abstract class BaseJMEntity
    {
        public BaseJMEntity()
        {
            CreatedDate = DateTime.UtcNow;
            UpdatedDate = DateTime.UtcNow;
        }
        [Key]
        public Guid Id { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatedUserId { get; set; }
        public Guid CompanyId { get; set; }
        public bool IsDelete { get; set; }
        [ForeignKey("CreatedUserId")]
        public virtual JM_Account User { get; set; }
    }
}
