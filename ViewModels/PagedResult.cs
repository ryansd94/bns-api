﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BNS.ViewModels
{
    public class PagedResult<T> : PagedResultBase
    {
        public List<T> Items { set; get; }
    }
}
