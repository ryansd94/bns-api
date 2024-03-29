﻿using BNS.Data.Entities.JM_Entities;
using BNS.Domain;
using GraphQL.Types;

namespace BNS.Service.Features.GraphqlCore
{
  public class JM_TeamType : ObjectGraphType<JM_Team>
    {
        public JM_TeamType(IGenericRepository<JM_Team> repository)
        {
            Field(x => x.Id).Description("id.");
            Field(x => x.Name).Description("name.");

            Field<ListGraphType<JM_AccountType>>(
              "accounts",
              arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "email" }),
              resolve: context => repository.FirstOrDefaultAsync()
           );
        }
    }
}
