using BNS.Data.Entities.JM_Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace BNS.Application.Extensions
{
    public static class ElasticsearchExtensions
    {
        public static void AddElasticsearch(
           this IServiceCollection services, IConfiguration configuration)
        {
            var url = configuration["DefaultConfig:Elasticsearch:url"];
            var defaultIndex = configuration["DefaultConfig:Elasticsearch:index"];

            var settings = new ConnectionSettings(new Uri(url))
                .DefaultIndex(defaultIndex).DefaultMappingFor<JM_Team>(m => m
        .IndexName("js_teams")).DefaultMappingFor<JM_Template>(m => m
        .IndexName("js_templates")
    ); ;

            var client = new ElasticClient(settings);

            services.AddSingleton<IElasticClient>(client);

            CreateIndex(client, defaultIndex);
        }

        private static void CreateIndex(IElasticClient client, string indexName)
        {
            var createIndexResponse = client.Indices.Create(indexName,
                index => index.Map<JM_Team>(x => x.AutoMap())
            );
        }
    }
}
