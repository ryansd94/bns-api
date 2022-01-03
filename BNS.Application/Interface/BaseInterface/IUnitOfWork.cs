using System;
using System.Collections.Generic;
using System.Text;

namespace BNS.Application.Interface
{
    public interface IUnitOfWork: IDisposable
    {
        ICF_AccountService CF_AccountRepository { get; }
        int Complete();
    }
}
