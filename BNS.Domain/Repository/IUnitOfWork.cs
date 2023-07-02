using BNS.Data.Entities.JM_Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace BNS.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<JM_Account> JM_AccountRepository { get; }
        IGenericRepository<JM_Company> JM_CompanyRepository { get; }
        IGenericRepository<JM_AccountCompany> JM_AccountCompanyRepository { get; }
        IGenericRepository<JM_Team> JM_TeamRepository { get; }
        IGenericRepository<JM_Project> JM_ProjectRepository { get; }
        IGenericRepository<JM_ProjectTeam> JM_ProjectTeamRepository { get; }
        IGenericRepository<JM_ProjectMember> JM_ProjectMemberRepository { get; }


        DbSet<T> Repository<T>() where T : class;
        Task<ApiResult<Guid>> SaveChangesAsync();
    }
}
