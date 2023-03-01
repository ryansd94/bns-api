using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BNS.Data.Entities.JM_Entities
{
    public class JM_Comment : BaseJMEntity
    {
        public string Value { get; set; }
        public int CountReply { get; set; }
        public int Level { get; set; }
        public Guid? ParentId { get; set; }

        [ForeignKey("ParentId")]
        public virtual JM_Comment CommentParent { get; set; }
        public virtual IEnumerable<JM_Comment> Chidrens { get; set; }
    }
}
