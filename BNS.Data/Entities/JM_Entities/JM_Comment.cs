using System;

namespace BNS.Data.Entities.JM_Entities
{
    public class JM_Comment: BaseJMEntity
    {
        public string Value { get; set; }
        public Guid? ParentId { get; set; }
    }
}
