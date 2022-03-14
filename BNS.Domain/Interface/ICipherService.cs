using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BNS.Domain
{
    public interface ICipherService
    { 
        string EncryptString(string plainText);
        Task<string> DecryptString(string cipherText);
    }
}
