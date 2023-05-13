using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using static BNS.Utilities.Enums;

namespace BNS.Data.Entities.JM_Entities
{
    [Index(nameof(ViewPermissionId), Name = "Nidx_SYS_ViewPermissionAction_ViewPermissionId")]
    public class SYS_ViewPermissionAction : BaseJMEntity
    {
        public EControllerKey Controller { get; set; }
        public Guid ViewPermissionId { get; set; }
        [ForeignKey("ViewPermissionId")]
        public virtual SYS_ViewPermission ViewPermission { get; set; }
        public virtual ICollection<SYS_ViewPermissionActionDetail> ViewPermissionActionDetails { get; set; }
    }
}
