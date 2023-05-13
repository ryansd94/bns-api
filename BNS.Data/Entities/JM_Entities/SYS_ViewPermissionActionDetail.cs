using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using static BNS.Utilities.Enums;

namespace BNS.Data.Entities.JM_Entities
{
    [Index(nameof(ViewPermissionActionId), Name = "Nidx_SYS_ViewPermissionActionDetail_ViewPermissionActionId")]
    public class SYS_ViewPermissionActionDetail: BaseJMEntity
    {
        public EActionType Key { get; set; }
        public bool? Value { get; set; }
        public Guid ViewPermissionActionId { get; set; }
        [ForeignKey("ViewPermissionActionId")]
        public virtual SYS_ViewPermissionAction ViewPermissionAction { get; set; }
    }
}
