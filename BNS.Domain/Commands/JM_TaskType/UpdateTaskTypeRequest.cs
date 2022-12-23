using System;
using System.ComponentModel.DataAnnotations;

namespace BNS.Domain.Commands
{
    public class UpdateTaskTypeRequest : CommandUpdateBase<ApiResult<Guid>>
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public string Color { get; set; }
        public string ColorFilter { get; set; }
        public int? Order { get; set; }
        public Guid? TemplateId { get; set; }
    }
}
