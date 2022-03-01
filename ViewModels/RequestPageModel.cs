using System;
using System.Collections.Generic;
using static BNS.Utilities.Enums;

namespace BNS.Models
{
   public class RequestPageModel<T> : BaseRequestModel
    {
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public bool isAdd { get; set; }
        public bool isEdit { get; set; }

        public List<columnField> columns { get; set; }
        public List<orderField> order { get; set; }
        public searchField search { get; set; }
        public List<sortField> sortModel { get; set; }
        public T filter { get; set; }
    }
    public class orderField
    {
        public int column { get; set; }
        public ESortEnum dir { get; set; }
    }
    public class columnField
    {
        public string data { get; set; }
        public string name { get; set; }
        public bool? searchable { get; set; }
        public bool? orderable { get; set; }
        public searchField search { get; set; }
    }
    public class sortField
    {
        public string field { get; set; }
        public string sort { get; set; }
    }
    public class searchField
    {
        public string value { get; set; }
        public bool? regex { get; set; }
        
    }
}
