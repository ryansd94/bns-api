﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BNS.ViewModels.Responses.Category
{
    public class JM_TeamResponse
    {
        public List<JM_TeamResponseItem> Items { get; set; }
    }

    public class JM_TeamResponseItem : BaseResultModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
    }
}