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
    public class ClientApprovalCommandHandler : IRequestHandler<CreateClientApprovalCommand, bool>
    {
        private readonly IEventBus _bus;

        public ClientApprovalCommandHandler(IEventBus bus)
        {
            _bus = bus;
        }
        public Task<bool> Handle(CreateClientApprovalCommand request, CancellationToken cancellationToken)
        {
            // --- Publish event to RabbitMQ
            var req = new ClientApprovalEvent(request.Id, request.FirstName, request.LastName, request.Mail, request.Phone);
            _bus.PublishAsync(req);

            return Task.FromResult(true);
        }
    }
}
