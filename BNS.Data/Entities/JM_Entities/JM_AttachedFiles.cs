using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BNS.Data.Entities.JM_Entities
{
    public class JM_AttachedFiles: BaseJMEntity
    {
        public Guid EntityId { get; set; }
        public Guid FileId { get; set; }
        [ForeignKey("FileId")]
        public virtual JM_File File { get; set; }
    }
}
