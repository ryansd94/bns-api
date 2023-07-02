using BNS.Domain.Responses;
using System.ComponentModel.DataAnnotations;

namespace BNS.Domain.Commands
{
    public class CheckOrganizationRequest : CommandBase<ApiResult<CheckOrganizationResponse>>
    {
        [Required]
        public string Domain { get; set; }
    }
}
