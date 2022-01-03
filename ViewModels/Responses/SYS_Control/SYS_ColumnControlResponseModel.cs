using System;
using System.Collections.Generic;
using System.Text;

namespace BNS.ViewModels.Responses
{
  public  class SYS_ColumnControlResponseModel
    {
        public string action { get; set; }
        public List<ColumnControl> field { get; set; }
        public List<ColumnControl> fieldRequire { get; set; }
    }
    public class ColumnControl
    {
        public string Column { get; set; }
        public string Order { get; set; }
    }

}
