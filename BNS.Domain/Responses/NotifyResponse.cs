using System;
using System.Collections.Generic;
using static BNS.Utilities.Enums;

namespace BNS.Domain.Responses
{
    public class NotifyResponse
    {
        public Guid Id { get; set; }
        public Guid? ObjectId { get; set; }
        public ENotifyObjectType Type { get; set; }
        public List<string> Contents { get; set; }
        public string AccountCompanyId { get; set; }
    }
}
