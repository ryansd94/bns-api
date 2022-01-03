using System;
using System.Collections.Generic;
using System.Text;

namespace BNS.ViewModels.Responses
{
    public class SYS_FieldControlResponseModel
    {
        public string action { get; set; }
        public List<FieldControl> field { get; set; }
    }


    public class FieldControl
    {
        public string Column { get; set; }
    }

}
