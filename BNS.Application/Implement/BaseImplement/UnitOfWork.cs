using BNS.Application.Interface;
using BNS.Data.Entities.JM_Entities;
using BNS.Data.EntityContext;
using System;
using System.Threading.Tasks;

namespace BNS.Application.Implement
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BNSDbContext _context;

        private IGenericRepository<JM_Account> jM_AccountRepository;
        private IGenericRepository<JM_AccountCompany> jM_AccountCompanyRepository;
        private IGenericRepository<JM_Team> jM_TeamRepository;
        private IGenericRepository<JM_TeamMember> jM_TeamMemberRepository;
        private IGenericRepository<JM_Sprint> jM_SprintRepository;

        #region Repositories
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
        public IGenericRepository<JM_TeamMember> JM_TeamMemberRepository
        {
            get
            {

                if (this.jM_TeamMemberRepository == null)
                {
                    this.jM_TeamMemberRepository = new GenericRepository<JM_TeamMember>(_context);
                }
                return jM_TeamMemberRepository;
            }
        }
        public IGenericRepository<JM_Team> JM_TeamRepository
        {
            get
            {

                if (this.jM_TeamRepository == null)
                {
                    this.jM_TeamRepository = new GenericRepository<JM_Team>(_context);
                }
                return jM_TeamRepository;
            }
        }
        public IGenericRepository<JM_Sprint> JM_SprintRepository
        {
            get
            {
                if (this.jM_SprintRepository == null)
                {
                    this.jM_SprintRepository = new GenericRepository<JM_Sprint>(_context);
                }
                return jM_SprintRepository;
            }
        }
        #endregion
        public UnitOfWork(BNSDbContext bNSDbContext)
        {
            this._context = bNSDbContext;
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
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
