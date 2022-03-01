using BNS.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace BNS.Domain.Commands
{
    public class CreateJM_SprintRequest : CommandBase<ApiResult<Guid>>
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public Guid JM_ProjectId { get; set; }
    }
}
