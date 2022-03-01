using System;
using System.Collections.Generic;
using System.Text;

namespace BNS.Models.Requests
{
    public class SYS_ControlModel: BaseRequestModel
    {
        public string ActionName { get; set; }
    }

    public class SYS_FieldModel : BaseRequestModel
    {
        public string ActionName { get; set; }
        public string FieldName { get; set; }
        public bool Hidden { get; set; }
    }

}
