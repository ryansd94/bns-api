using System;
using System.Collections.Generic;
using static BNS.Utilities.Enums;

namespace BNS.Domain.Responses
{
    public class CustomColumnsResponse
    {
        public List<CustomColumnsResponseItem> Items { get; set; }
    }
    public class CustomColumnsResponseItem : BaseResponseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public EControlType ControlType { get; set; }
    }
}
