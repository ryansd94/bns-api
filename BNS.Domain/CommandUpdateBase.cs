using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static BNS.Utilities.Enums;

namespace BNS.Domain
{
    public class CommandUpdateBase<T> : CommandBase<T> where T : class
    {
        [Required]
        public Guid Id { get; set; }
        public List<ChangeFieldItem> ChangeFields { get; set; }
    }

    public class ChangeFieldItem
    {
        public string Key { get; set; }
        public object Value { get; set; }
        public EControlType Type { get; set; }
    }

    public class ChangeFieldTransferItem
    {
        public List<Guid> AddValues { get; set; }
        public List<Guid> DeleteValues { get; set; }
    }
}
