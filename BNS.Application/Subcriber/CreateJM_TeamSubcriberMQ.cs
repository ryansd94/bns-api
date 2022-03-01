using BNS.Domain.Events;
using System;
using System.Collections.Generic;

namespace BNS.Service.Subcriber
{
    public class CreateJM_TeamSubcriberMQ : BaseCompletedEvent
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public Guid? ParentId { get; set; }
        public List<Guid> Members { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid CompanyId { get; set; }
    }
}
