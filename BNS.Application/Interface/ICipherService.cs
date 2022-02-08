﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BNS.Application.Interface
{
    public interface ICipherService
    { 
        string EncryptString(string plainText);
        string DecryptString(string cipherText);
    }
}
