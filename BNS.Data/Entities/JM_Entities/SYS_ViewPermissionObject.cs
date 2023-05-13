using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using static BNS.Utilities.Enums;

namespace BNS.Data.Entities.JM_Entities
{
    [Index(nameof(ViewPermissionId), Name = "Nidx_SYS_ViewPermissionObject_ViewPermissionId")]
    public class SYS_ViewPermissionObject : BaseJMEntity
    {
        public Guid ObjectId { get; set; }
        public EPermissionObject ObjectType { get; set; }
        public Guid ViewPermissionId { get; set; }
        [ForeignKey("ViewPermissionId")]
        public virtual SYS_ViewPermission ViewPermission { get; set; }
    }
}
