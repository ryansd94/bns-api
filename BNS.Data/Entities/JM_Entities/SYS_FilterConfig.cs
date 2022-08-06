using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BNS.Data.Entities.JM_Entities
{
    public class SYS_FilterConfig : BaseJMEntity
    {
        public string Name { get; set; }
        public string FilterData { get; set; }
        public string View { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual JM_Account JM_Account { get; set; }
    }
}
