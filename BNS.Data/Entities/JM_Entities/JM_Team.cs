using BNS.Data.Entities.JM_Entities;
using System;
namespace BNS.Data.Entities
{
    public partial class JM_Team : BaseJMEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public CF_Shop CF_Shop { get; set; }
        public Guid? ParentId { get; set; }

        public CF_Account CreateUserAccount { get; set; }
        public CF_Account UpdateUserAccount { get; set; }
    }
}
