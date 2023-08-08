using System;
using BNS.Data.Entities.JM_Entities;
using BNS.Domain.Events;

namespace BNS.Service.Subcriber
{
    public class SetTaskNumberSubcriber: BaseCompletedEvent
    {
        public Guid CompanyId { get; set; }
        public JM_Task Task { get; set; }
    }
}
