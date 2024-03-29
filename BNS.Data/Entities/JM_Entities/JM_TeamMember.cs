﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BNS.Data.Entities.JM_Entities
{
    public partial class JM_TeamMember : BaseJMEntity
    {
        public Guid TeamId { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual JM_Account JM_Account { get; set; }
        [ForeignKey("TeamId")]
        public virtual JM_Team JM_Team { get; set; }
    }
}
