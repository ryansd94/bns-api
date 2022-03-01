using BNS.Application.Features.GraphqlCore;
using BNS.Data.Entities.JM_Entities;
using BNS.Domain;
using GraphQL;
using GraphQL.Types;
using System;

namespace BNS.Application.Features.GraphQL.Commands
{
   public class JM_TeamEventQuery : ObjectGraphType<object>
    {
        public JM_TeamEventQuery(IGenericRepository<JM_Team> repository)
        {
            Name = "TechEventQuery";

            Field<JM_TeamType>(
               "jm_team",
               arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "id" }),
               resolve: context => repository.GetByIdAsync(context.GetArgument<Guid>("id"))
            );

            Field<ListGraphType<JM_TeamType>>(
             "jm_teams",
             resolve: context => repository.GetAsync()
          );
        }
    }
}
