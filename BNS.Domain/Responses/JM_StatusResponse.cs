﻿using System.Collections.Generic;

namespace BNS.Domain.Responses
{
    public class JM_StatusResponse
    {
        public List<StatusResponseItem> Items { get; set; }
    }

    public class StatusResponseItem : BaseResponseModel
    {
        public string Name { get; set; }
        public string Color { get; set; }
    }
}
