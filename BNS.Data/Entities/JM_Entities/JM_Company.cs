using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BNS.Data.Entities.JM_Entities
{
    public partial class JM_Company
    {
        [Key]
        public Guid Index { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Domain { get; set; }
        public string Address { get; set; }
        public double? Lat { get; set; }
        public double? Long { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatedUser { get; set; }
        public bool IsDelete { get; set; }
    }
}
