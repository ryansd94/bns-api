using static BNS.Utilities.Enums;

namespace BNS.Domain.Responses
{
    public class CompanyResponse
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public EManagementType ManagementType { get; set; }
    }
}
