
using System.Collections.Generic;

namespace BNS.Domain
{
    public class CommandCreateBase<T> : CommandBase<T> where T : class
    {
        public List<T> Items { get; set; }
    }
}
