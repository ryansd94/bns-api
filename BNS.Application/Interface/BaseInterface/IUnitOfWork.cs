using BNS.Data.Entities.JM_Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BNS.Application.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<JM_Account> JM_AccountRepository { get; }
        IGenericRepository<JM_AccountCompany> JM_AccountCompanyRepository { get; }
        int Complete();
    }
}
