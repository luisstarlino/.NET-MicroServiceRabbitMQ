using MediatR;
using MicroRabbit.Banking.Domain.Commands;
using MicroRabbit.Banking.Domain.Events;
using MicroRabbit.Domain.Core.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroRabbit.Banking.Domain.CommandHandlers
{
    public class TransferCommandHandler : IRequestHandler<CreateTransferCommand, bool>
    {
        private readonly IEventBus _bus;

        public TransferCommandHandler(IEventBus bus)
        {
            _bus = bus;
        }

        public Task<bool> Handle(CreateTransferCommand req, CancellationToken cancellationToken)
        {
            // --- Publish event to RabbitMQ
            _bus.PublishAsync(new TransferCreatedEvent(req.From, req.To, req.Amount));

            return Task.FromResult(true);
        }
    }
}
