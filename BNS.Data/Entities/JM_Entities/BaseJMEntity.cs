using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BNS.Data.Entities.JM_Entities
{
    public abstract class BaseJMEntity
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatedUser { get; set; }
        public Guid? CompanyId { get; set; }
        public bool IsDelete { get; set; }
        [ForeignKey("CompanyId")]
        public virtual JM_Company JM_Company { get; set; }

    }
}
