using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace BNS.Utilities.Interface
{
    public interface ICaptcha
    {
        byte[] GenerateImage(string text, int width, int height, string familyName);
    }
}
