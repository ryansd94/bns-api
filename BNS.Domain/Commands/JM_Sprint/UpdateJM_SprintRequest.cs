using System;
using System.ComponentModel.DataAnnotations;
namespace BNS.Domain.Commands
{
    public class UpdateJM_SprintRequest : CommandBase<ApiResult<Guid>>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public Guid Id { get; set; }
        public string Description { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
    }
}
