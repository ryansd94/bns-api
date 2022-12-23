using System;
using System.ComponentModel.DataAnnotations;

namespace BNS.Domain
{
    public class CommandUpdateBase<T> : CommandBase<T> where T : class
    {
        [Required]
        public Guid Id { get; set; }
    }
}
