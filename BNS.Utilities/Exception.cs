using System;
using System.Collections.Generic;
using System.Text;

namespace BNS.Utilities
{
   public class UtilityException : Exception
    {
        public UtilityException()
        { 
        }
        public UtilityException(string messgae):base(messgae)
        {
        }
        public UtilityException(string messgae,Exception inner) : base(messgae, inner)
        {
        }
    }
}
