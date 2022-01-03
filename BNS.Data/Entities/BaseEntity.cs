using BNS.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BNS.Data
{
   public class BaseEntity
    {
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatedUser { get; set; }
        public Guid ShopIndex { get; set; }
        public bool? IsDelete { get; set; }
        public Guid? BranchIndex { get; set; }
        [Key]
        public Guid Index { get; set; }


    }
}
