using System;
using System.ComponentModel.DataAnnotations.Schema;
using static BNS.Utilities.Enums;

namespace BNS.Data.Entities.JM_Entities
{
    public class JM_NotifycationUser: BaseJMEntity
    {
        public Guid? ObjectId { get; set; }
        public ENotifyObjectType Type { get; set; }
        public string Content { get; set; }
        public Guid UserReceivedId { get; set; }
        public bool IsRead { get; set; }
        [ForeignKey("UserReceivedId")]
        public virtual JM_Account UserReceived { get; set; }
    }
}
