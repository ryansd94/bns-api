using BNS.Data.Entities.JM_Entities;
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
        IGenericRepository<JM_TeamMember> JM_TeamMemberRepository { get; }
        IGenericRepository<JM_Sprint> JM_SprintRepository { get; }
        IGenericRepository<JM_Project> JM_ProjectRepository { get; }
        IGenericRepository<JM_ProjectTeam> JM_ProjectTeamRepository { get; }
        IGenericRepository<JM_ProjectMember> JM_ProjectMemberRepository { get; }
        IGenericRepository<JM_Template> JM_TemplateRepository { get; }


        Task<ApiResult<Guid>> SaveChangesAsync();
    }
}
