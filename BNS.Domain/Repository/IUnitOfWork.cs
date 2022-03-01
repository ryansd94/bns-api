using BNS.Data.Entities.JM_Entities;
using BNS.Models;
using System;
using System.Threading.Tasks;

namespace BNS.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<JM_Account> JM_AccountRepository { get; }
        IGenericRepository<JM_AccountCompany> JM_AccountCompanyRepository { get; }
        IGenericRepository<JM_Team> JM_TeamRepository { get; }
        IGenericRepository<JM_TeamMember> JM_TeamMemberRepository { get; }
        IGenericRepository<JM_Sprint> JM_SprintRepository { get; }


        Task<ApiResult<Guid>> SaveChangesAsync();
    }
}
