using System.Collections.Generic;

namespace BNS.Data.Entities.JM_Entities
{
    public class SYS_ViewPermission : BaseJMEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<SYS_ViewPermissionAction> ViewPermissionActions { get; set; }
        public virtual ICollection<SYS_ViewPermissionObject> ViewPermissionObjects { get; set; }
    }
}
