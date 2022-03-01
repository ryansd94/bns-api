using System;
using System.Collections.Generic;
using System.Text;

namespace BNS.Models.Responses.Menu
{
   public class MenuResponseModel
    {
        public string page { get; set; }
        public string header { get; set; }
        public string icon { get; set; }
        public string action { get; set; }
        public List<ChildPage> childPage { get; set; }
        public List<Button> button { get; set; }
    }

    public class ChildPage
    {
        public string page { get; set; }
        public string header { get; set; }
        public string icon { get; set; }
        public string action { get; set; }
        public List<ChildPage> childPage { get; set; }
        public List<Button> button { get; set; }
    }
    public class Button
    {
        public string key { get; set; }
        public string text { get; set; }
    }
}
