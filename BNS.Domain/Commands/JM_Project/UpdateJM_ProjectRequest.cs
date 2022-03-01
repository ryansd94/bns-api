using BNS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BNS.Domain.Commands.JM_Project
{
    public class UpdateJM_ProjectRequest : CommandBase<ApiResult<Guid>>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<Guid> Teams { get; set; }
        public List<Guid> Members { get; set; }
    }
}
