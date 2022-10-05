using static BNS.Utilities.Enums;

namespace BNS.Data.Entities.JM_Entities
{
    public class JM_CustomColumn : BaseJMEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public EControlType ControlType { get; set; }
    }
}
