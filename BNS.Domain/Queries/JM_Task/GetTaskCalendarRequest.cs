using BNS.Domain.Responses;
using System;

namespace BNS.Domain.Queries
{
    public class GetTaskCalendarRequest: CommandGetRequest<ApiResultList<TaskCalendarRespone>>
    {
        public Guid? ProjectId { get; set; }
    }
}
