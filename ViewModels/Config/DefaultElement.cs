using System;
using System.Collections.Generic;
using System.Text;

namespace BNS.ViewModels
{
   public class DefaultElement
    {
        public string FullDomain { get; set; } = "https://localhost:9004";
        public int NumberOfTrialDay { get; set; } = 14;
        public int BranchMaxLengthView { get; set; } = 10;
        public string WebUserDomain { get; set; } = "https://bns-user-test.herokuapp.com";
        public string CipherKey { get; set; } = "BNS Key";
    }
}
