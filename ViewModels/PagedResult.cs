using System;
using System.Collections.Generic;
using System.Text;

namespace BNS.Models
{
    public class PagedResult<T> : PagedResultBase
    {
        public List<T> Items { set; get; }
    }
}
