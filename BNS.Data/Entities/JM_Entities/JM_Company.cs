using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BNS.Data.Entities.JM_Entities
{
    public partial class JM_Company
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(500)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string Code { get; set; }
        [MaxLength(100)]
        public string Domain { get; set; }
        public string Address { get; set; }
        public double? Lat { get; set; }
        public double? Long { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatedUser { get; set; }
        public bool IsDelete { get; set; }
        public virtual ICollection<JM_Account> JM_Accounts { get; set; }
        public virtual ICollection<JM_AccountCompany> JM_AccountCompanys { get; set; }
    }
}
