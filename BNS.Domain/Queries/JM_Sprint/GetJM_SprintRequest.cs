using BNS.Domain.Responses;
using System;
using System.ComponentModel.DataAnnotations;

namespace BNS.Domain.Queries
{
    public class GetJM_SprintRequest : CommandGetRequest<ApiResult<JM_SprintResponse>>
    {
        [Required]
        public Guid JM_ProjectId { get; set; }
    }
}
