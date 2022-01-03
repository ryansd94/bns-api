using BNS.Application.Interface;
using BNS.Data.EntityContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace BNS.Application.Implement
{
   public  class UnitOfWork : IUnitOfWork
    {
        private readonly BNSDbContext _context;

        public ICF_AccountService CF_AccountRepository { get; }

        public UnitOfWork(BNSDbContext bNSDbContext,
            ICF_AccountService CF_AccountRepository)
        {
            this._context = bNSDbContext;
            this.CF_AccountRepository = CF_AccountRepository;
        }
        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}
