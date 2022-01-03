using System;
using System.Collections.Generic;
using System.Text;
using static BNS.Utilities.Enums;

namespace BNS.ViewModels
{
    public class BaseRequestModel
    {
        public BaseRequestModel()
        {
            lang = ELang.vi.ToString();
        }

        public Guid UserIndex { get; set; }
        public Guid Index { get; set; }
        public string lang { get; set; }
        public Guid ShopIndex { get; set; }
        public List<Guid> BranchIndexs { get; set; }
        public Guid? BranchIndex { get; set; }
        public string ModelTitle { get; set; }
        public string ModelLabelClass { get; set; }
    }
}
