using System;
using System.Collections.Generic;
using static BNS.Utilities.Enums;

namespace BNS.Data.Entities.JM_Entities
{
    public class JM_Notifycation : BaseJMEntity
    {
        public Guid? ObjectId { get; set; }
        public ENotifyObjectType Type { get; set; }
        public string Content { get; set; }
    }
}
