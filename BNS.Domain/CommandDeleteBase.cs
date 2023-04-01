using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BNS.Domain
{
    public class CommandDeleteBase<T> : CommandBase<T> where T : class
    {
        [Required]
        public List<Guid> ids { get; set; }
    }
}
