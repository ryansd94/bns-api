using System;

namespace BNS.ViewModels.Responses.Category
{
    public class CF_BranchResponseModel : BaseResultModel
    {
        public string Name { get; set; }
        public int? Number { get; set; }
        public string Note { get; set; }
        public bool? IsMaster { get; set; }
        public bool? IsDefault { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
    public class CF_BranchDefaultResponseModel
    {
        public string Name { get; set; }
        public Guid Index { get; set; }
        public bool Checked { get; set; }
    }
}
