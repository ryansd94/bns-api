﻿using System;
using System.Reflection;
using System.Threading.Tasks;
using BNS.Domain.Messaging;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;
using RawRabbit.Configuration.Exchange;
using Serilog;

namespace BNS.Infrastructure.RabbitMq
{
    public class BusSubscriber : IBusSubscriber
    {
        private readonly IBusClient _busClient;
        private readonly IServiceProvider _serviceProvider;

        public BusSubscriber(IApplicationBuilder app)
        {
            _serviceProvider = app.ApplicationServices.GetService<IServiceProvider>();
            _busClient = _serviceProvider?.GetService<IBusClient>();
        }

        public async Task<IBusSubscriber> SubscribeEvent<TEvent>() where TEvent : IEvent, IRequest
        {
            if (_busClient != null)
            {
                await _busClient.SubscribeAsync<TEvent>(async (@event) =>
                 {
                     try
                     {
                         using var scope = _serviceProvider.CreateScope();
                         var handler = scope.ServiceProvider.GetService<IMediator>();
                         await handler.Send(@event);
                     }
                     catch (Exception ex)
                     {
                         Log.Error(ex, "Erro ao processar mensagem");
                         throw;
                     }

                 }, ctx => ctx.UseSubscribeConfiguration(cfg => cfg
                     .Consume(c => c.WithRoutingKey(typeof(TEvent).Name))
                     .FromDeclaredQueue(q => q
                         .WithName(GetQueueName<TEvent>())
                         .WithDurability()
                         .WithAutoDelete(false))
                     .OnDeclaredExchange(e => e
                       .WithName("sample-rabbitmq-publish")
                       .WithType(ExchangeType.Topic)
                       .WithArgument("key", typeof(TEvent).Name.ToLower()))
                 ));
                return this;
            }
            else return null;
        }

        private static string GetQueueName<T>() => $"{Assembly.GetEntryAssembly()?.GetName().Name}/{typeof(T).Name}";
    }
}
