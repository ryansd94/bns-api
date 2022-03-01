using BNS.Domain.Events;
using System;
using System.Collections.Generic;

namespace BNS.Service.Subcriber
{
    public class SendMailSubcriberMQ : BaseCompletedEvent
    {
        public List<SendMailSubcriberMQItem> Items { get; set; }
    }
    public class SendMailSubcriberMQItem
    {
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

    }
}
