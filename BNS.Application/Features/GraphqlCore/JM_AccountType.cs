using BNS.Data.Entities.JM_Entities;
using BNS.Domain;
using GraphQL.Types;

namespace BNS.Application.Features.GraphqlCore
{
    public class JM_AccountType : ObjectGraphType<JM_Account>
    {
        public JM_AccountType(IGenericRepository<JM_Account> repository)
        {
            Field(x => x.Id).Description("Id.");
            Field(x => x.Email).Description("Email.");
        }
    }
}
