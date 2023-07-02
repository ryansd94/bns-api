using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static BNS.Utilities.Enums;

namespace BNS.Domain.Commands
{
    public class CreateProjectRequest : CommandBase<ApiResult<Guid>>
    {
        [Required]
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public EProjectType Type { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<Guid> Teams { get; set; }
        public List<Guid> Members { get; set; }
        public List<SprintRequest> Sprints { get; set; }
    }

    public class SprintRequest
    {
        [Required]
        public string Name { get; set; }
        public Guid? Id { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public ERowStatus? RowStatus { get; set; }
        public Guid? ParentId { get; set; }
        public List<SprintRequest> Childs { get; set; }
        public bool Active { get; set; }
    }
}
