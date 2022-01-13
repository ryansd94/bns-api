using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BNS.Data.Entities.JM_Entities
{
    public partial class JM_Team : BaseJMEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public Guid? ParentId { get; set; }

        [ForeignKey("ParentId")]
        public JM_Team TeamParent { get; set; }

        public JM_Account CreateUserAccount { get; set; }
        public JM_Account UpdateUserAccount { get; set; }
    }
}
