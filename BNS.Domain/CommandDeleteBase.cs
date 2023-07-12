using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BNS.Domain
{
    public class CommandDeleteBase<T> : CommandBase<T> where T : class
    {
        public CommandDeleteBase()
        {
            Ids = new List<Guid>();
        }
        [Required]
        public List<Guid> Ids { get; set; }
    }
}
