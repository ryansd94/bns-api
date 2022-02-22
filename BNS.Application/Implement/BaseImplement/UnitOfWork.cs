using BNS.Application.Interface;
using BNS.Data.Entities.JM_Entities;
using BNS.Data.EntityContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace BNS.Application.Implement
{
   public  class UnitOfWork : IUnitOfWork
    {
        private readonly BNSDbContext _context;

        private IGenericRepository<JM_Account> jM_AccountRepository;

        public IGenericRepository<JM_Account> JM_AccountRepository
        {
            get
            {

                if (this.jM_AccountRepository == null)
                {
                    this.jM_AccountRepository = new GenericRepository<JM_Account>(_context);
                }
                return jM_AccountRepository;
            }
        }


        private IGenericRepository<JM_AccountCompany> jM_AccountCompanyRepository;

        public IGenericRepository<JM_AccountCompany> JM_AccountCompanyRepository
        {
            get
            {

                if (this.jM_AccountCompanyRepository == null)
                {
                    this.jM_AccountCompanyRepository = new GenericRepository<JM_AccountCompany>(_context);
                }
                return jM_AccountCompanyRepository;
            }
        }
        public UnitOfWork(BNSDbContext bNSDbContext)
        {
            this._context = bNSDbContext;
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
