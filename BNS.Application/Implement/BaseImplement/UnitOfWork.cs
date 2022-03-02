using BNS.Data.Entities.JM_Entities;
using BNS.Data.EntityContext;
using BNS.Domain;
using System;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;

namespace BNS.Service.Implement
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BNSDbContext _context;

        private IGenericRepository<JM_Account> jM_AccountRepository;
        private IGenericRepository<JM_AccountCompany> jM_AccountCompanyRepository;
        private IGenericRepository<JM_Team> jM_TeamRepository;
        private IGenericRepository<JM_TeamMember> jM_TeamMemberRepository;
        private IGenericRepository<JM_Sprint> jM_SprintRepository;
        private IGenericRepository<JM_Company> jM_CompanyRepository;
        private IGenericRepository<JM_Project> jM_ProjectRepository;
        private IGenericRepository<JM_ProjectMember> jM_ProjectMemberRepository;
        private IGenericRepository<JM_ProjectTeam> jM_ProjectTeamRepository;


        #region Repositories
        public IGenericRepository<JM_ProjectMember> JM_ProjectMemberRepository
        {
            get
            {

                if (this.jM_ProjectMemberRepository == null)
                {
                    this.jM_ProjectMemberRepository = new GenericRepository<JM_ProjectMember>(_context);
                }
                return jM_ProjectMemberRepository;
            }
        }
        public IGenericRepository<JM_ProjectTeam> JM_ProjectTeamRepository
        {
            get
            {

                if (this.jM_ProjectTeamRepository == null)
                {
                    this.jM_ProjectTeamRepository = new GenericRepository<JM_ProjectTeam>(_context);
                }
                return jM_ProjectTeamRepository;
            }
        }
        public IGenericRepository<JM_Project> JM_ProjectRepository
        {
            get
            {

                if (this.jM_ProjectRepository == null)
                {
                    this.jM_ProjectRepository = new GenericRepository<JM_Project>(_context);
                }
                return jM_ProjectRepository;
            }
        }
        public IGenericRepository<JM_Company> JM_CompanyRepository
        {
            get
            {

                if (this.jM_CompanyRepository == null)
                {
                    this.jM_CompanyRepository = new GenericRepository<JM_Company>(_context);
                }
                return jM_CompanyRepository;
            }
        }
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
        public async Task<ApiResult<Guid>> SaveChangesAsync()
        {
            var result = new ApiResult<Guid>();
            result.status=System.Net.HttpStatusCode.OK;
            try
            {
                var rs = await _context.SaveChangesAsync();
                result.errorCode=EErrorCode.Success.ToString();
            }
            catch (Exception ex)
            {
                result.errorCode=EErrorCode.Failed.ToString();
                result.title=ex.Message;
            }
            return result;
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
