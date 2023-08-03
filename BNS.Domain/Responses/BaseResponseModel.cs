using System;
using System.Collections.Generic;

namespace BNS.Domain
{
    public class BaseResponseModel
    {
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
        public Guid CreatedUserId { get; set; }
        public Guid Id { get; set; }
        public Dictionary<string, string> DynamicData { get; set; }
    }
}
