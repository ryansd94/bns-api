using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BNS.Data.Entities.JM_Entities
{
    public class BaseJMEntity
    {
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatedUser { get; set; }
        public Guid? CompanyIndex { get; set; }
        public bool? IsDelete { get; set; }
        [Key]
        public Guid Index { get; set; }
    }
}
