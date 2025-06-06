﻿using MediatR;
using MicroRabbit.Domain.Core.Bus;
using MicroRabbit.Domain.Core.Commands;
using MicroRabbit.Domain.Core.Events;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroRabbit.Infra.Bus
{
    public sealed class RabbitMQBus : IEventBus
    {
        private readonly IMediator _mediator;
        private readonly Dictionary<string, List<Type>> _handlers;
        private readonly List<Type> _eventTypes;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public RabbitMQBus(IMediator mediator, IServiceScopeFactory serviceScopeFactory)
        {
            _mediator = mediator;
            _handlers = new Dictionary<string, List<Type>>();
            _eventTypes = new List<Type>();
            _serviceScopeFactory = serviceScopeFactory;
        }

        public Task SendCommand<T>(T command) where T : Command
        {
            return _mediator.Send(command);
        }

        public async Task PublishAsync<T>(T @event) where T : Event
        {
            //------------------------------------------------------------------------------------------------
            // Config
            //------------------------------------------------------------------------------------------------
            var eventName = @event.GetType().Name;
            var factory = new ConnectionFactory { HostName = "localhost" };
            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();
            await channel.QueueDeclareAsync(eventName, false, false, false, null);

            //------------------------------------------------------------------------------------------------
            // Set message
            //------------------------------------------------------------------------------------------------
            var message = JsonConvert.SerializeObject(@event);
            var body = Encoding.UTF8.GetBytes(message);

            //------------------------------------------------------------------------------------------------
            // Set Publish
            //------------------------------------------------------------------------------------------------
            await channel.BasicPublishAsync("", eventName, body);

        }

        public async Task Subscribe<T, TH>()
            where T : Event
            where TH : IEventHandler<T>
        {
            //------------------------------------------------------------------------------------------------
            // Config
            //------------------------------------------------------------------------------------------------
            var eventName = typeof(T).Name;
            var handlerType = typeof(TH);

            if (!_eventTypes.Contains(typeof(T))){
                _eventTypes.Add(typeof(T));
            }

            if (!_handlers.ContainsKey(eventName))
            {
                _handlers.Add(eventName, new List<Type>());
            }

            if (_handlers[eventName].Any(t => t.GetType() == handlerType))
            {
                throw new ArgumentException($"Handler Type {handlerType.Name} already is registered for '{eventName}'", nameof(handlerType));
            }

            _handlers[eventName].Add(handlerType);

            await StartBasicConsumeAsync<T>();
            
        }

        private async Task StartBasicConsumeAsync<T>() where T : Event
        {

            //------------------------------------------------------------------------------------------------
            // Creating a connection async
            //------------------------------------------------------------------------------------------------
            var factory = new ConnectionFactory 
            { 
                HostName = "localhost",
                ConsumerDispatchConcurrency = 1
            };
            var connection = await factory.CreateConnectionAsync();
            var channel = await connection.CreateChannelAsync();

            //------------------------------------------------------------------------------------------------
            // Queue Declare
            //------------------------------------------------------------------------------------------------
            var eventName = typeof(T).Name;
            await channel.QueueDeclareAsync(eventName, false, false, false, null);

            //------------------------------------------------------------------------------------------------
            // Create async consumer
            //------------------------------------------------------------------------------------------------
            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += Consumer_Received;

            //------------------------------------------------------------------------------------------------
            // Start consume
            //------------------------------------------------------------------------------------------------
            await channel.BasicConsumeAsync(eventName, true, consumer);

        }

        private async Task Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            var eventName = e.RoutingKey;
            var message = Encoding.UTF8.GetString(e.Body.ToArray());

            try
            {
                await ProcessEvent(eventName, message).ConfigureAwait(false);
            }
            catch (Exception ex) 
            {

            }
        }

        private async Task ProcessEvent(string eventName, string message)
        {
            if(_handlers.ContainsKey(eventName))
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var subscriptions = _handlers[eventName];
                    foreach (var subscription in subscriptions)
                    { 
                        var handler = scope.ServiceProvider.GetService(subscription);
                        if (handler == null) continue;
                        var eventType = _eventTypes.SingleOrDefault(t => t.Name == eventName);
                        var @event = JsonConvert.DeserializeObject(message, eventType!);
                        var conreteType = typeof(IEventHandler<>).MakeGenericType(eventType!);

                        await (Task)conreteType.GetMethod("Handle")!.Invoke(handler, new object[] { @event! })!;
                    }
                }
            }
        }
    }
}
