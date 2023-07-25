﻿using BNS.Domain.Messaging;
using BNS.Infrastructure.RabbitMq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;
using RawRabbit.Configuration;
using RawRabbit.Enrichers.GlobalExecutionId;
using RawRabbit.Enrichers.MessageContext;
using RawRabbit.Enrichers.MessageContext.Context;
using RawRabbit.Instantiation;
using System;

namespace BNS.Infrastructure.Messaging
{
    public static class RawRabbitInstaller
    {
        public static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            var options = new RawRabbitConfiguration();
            configuration.GetSection("DefaultConfig:RabbitMq").Bind(options);
            var client = RawRabbitFactory.CreateSingleton(new RawRabbitOptions
            {
                ClientConfiguration = options,
                Plugins = p => p
                    .UseGlobalExecutionId()
                    .UseHttpContext()
                    .UseMessageContext(c => new MessageContext { GlobalRequestId = Guid.NewGuid() })
            });

            services.AddSingleton<IBusClient>(_ => client);

            services.AddScoped<IBusPublisher, BusPublisher>();

            return services;
        }

        public static IBusSubscriber UseRabbitMq(this IApplicationBuilder app) => new BusSubscriber(app);
    }
}
