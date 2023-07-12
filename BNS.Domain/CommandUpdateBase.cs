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
        public bool IsEntity { get; set; } = true;
    }

    public class ChangeFieldTransferItem<T>
    {
        public List<T> AddValues { get; set; }
        public List<T> DeleteValues { get; set; }
    }
}
