using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MicroRabbit.Analytics.Data.Context;
using MicroRabbit.Analytics.Data.Repository;
using MicroRabbit.Analytics.Domain.EventHandlers;
using MicroRabbit.Analytics.Domain.Events;
using MicroRabbit.Analytics.Domain.Interfaces;
using MicroRabbit.Banking.Application.Interfaces;
using MicroRabbit.Banking.Application.Services;
using MicroRabbit.Banking.Data.Context;
using MicroRabbit.Banking.Data.Repository;
using MicroRabbit.Banking.Domain.CommandHandlers;
using MicroRabbit.Banking.Domain.Commands;
using MicroRabbit.Banking.Domain.Interfaces;
using MicroRabbit.Domain.Core.Bus;
using MicroRabbit.Infra.Bus;
using MicroRabbit.Transfer.Application.Interfaces;
using MicroRabbit.Transfer.Application.Services;
using MicroRabbit.Transfer.Data.Context;
using MicroRabbit.Transfer.Data.Repository;
using MicroRabbit.Transfer.Domain.EventHandlers;
using MicroRabbit.Transfer.Domain.Events;
using MicroRabbit.Transfer.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MicroRabbit.Infra.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            // --- Domain Bus
            services.AddSingleton<IEventBus, RabbitMQBus>(sp =>
            {
                var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
                return new RabbitMQBus(sp.GetService<IMediator>()!, scopeFactory);
            });

            #region ADD SUBSCRIPTIONS TO QUEUE's (Subscriptions)
            // --- (Transfer)
            services.AddTransient<TransferEventHandler>();

            // --- (Analytics)
            services.AddTransient<ClientApprovalHandler>();
            #endregion

            #region PREPARE DE QUEUE TO RECEIVE THE EVENT (Domain Events)
            // --- (Transfer)
            services.AddTransient<IEventHandler<TransferCreatedEvent>, TransferEventHandler>();

            // --- (Analytics)
            services.AddTransient<IEventHandler<ClientApprovalEvent>, ClientApprovalHandler>();
            #endregion

            #region LINK HANDLES + COMMANDS (TO SEND A REQUEST TO A QUEUE | Domain Banking Commands)
            // --- (Transfer)
            services.AddTransient<IRequestHandler<CreateTransferCommand, bool>, TransferCommandHandler>();

            // --- (Analytics)
            services.AddTransient<IRequestHandler<CreateClientApprovalCommand, bool>, ClientApprovalCommandHandler>();
            #endregion

            #region APPLICATION SERVICES
            // --- (Banking)
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IClientService, ClientService>();

            // --- (Transfer)
            services.AddTransient<ITransferService, TransferService>();
            #endregion

            #region DATA LAYER (LINK CONTEXT's)
            // --- (Banking)
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<IClientRepository, ClientRepository>();
            services.AddTransient<BankingDbContext>();

            // --- (Transfer)
            services.AddTransient<ITransferRepository, TransferRepository>();
            services.AddTransient<TransferDbContext>();

            // --- (Analytics)
            services.AddTransient<IAnalyticsRepository, AnalyticsRepository>();
            services.AddTransient<AnalyticsDbContext>();
            #endregion

        }
    }
}
