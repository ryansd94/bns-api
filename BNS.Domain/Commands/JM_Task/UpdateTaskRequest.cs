using System;
using System.ComponentModel.DataAnnotations;

namespace BNS.Domain.Commands
{
    public class UpdateTaskRequest : CreateTaskRequest
    {

        [Required]
        public Guid Id { get; set; }
    }
}
