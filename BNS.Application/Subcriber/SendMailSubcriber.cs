using BNS.Domain.Events;
using System;
using System.Collections.Generic;

namespace BNS.Service.Subcriber
{
    public class SendMailSubcriber : BaseCompletedEvent
    {
        public List<SendMailSubcriberItem> Items { get; set; }
    }
    public class SendMailSubcriberItem
    {
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

    }
}
