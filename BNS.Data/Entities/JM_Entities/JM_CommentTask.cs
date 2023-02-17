using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BNS.Data.Entities.JM_Entities
{
    public class JM_CommentTask : BaseJMEntity
    {
        public Guid TaskId { get; set; }
        public Guid CommentId { get; set; }
        public string Value { get; set; }
        [ForeignKey("TaskId")]
        public virtual JM_Task Task { get; set; }
        [ForeignKey("CommentId")]
        public virtual JM_Comment Comment { get; set; }
    }
}
