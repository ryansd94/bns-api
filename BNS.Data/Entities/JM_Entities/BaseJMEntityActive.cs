
using System.ComponentModel;

namespace BNS.Data.Entities.JM_Entities
{
    public abstract class BaseJMEntityActive : BaseJMEntity
    {
        public BaseJMEntityActive()
        {
            IsActive = true;
        }

        [DefaultValue("true")]
        public bool IsActive { get; set; }
    }
}
