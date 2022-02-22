using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static BNS.Utilities.Enums;

namespace BNS.Data.Entities.JM_Entities
{ 
    public partial class JM_AccountCompany  
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatedUser { get; set; }
        public bool IsDelete { get; set; }
        public Guid UserId { get; set; }
        public Guid CompanyId { get; set; }
        public EStatus Status { get; set; }
        public virtual JM_Company JM_Company { get; set; }
        public virtual JM_Account JM_Account { get; set; }
    }
}
