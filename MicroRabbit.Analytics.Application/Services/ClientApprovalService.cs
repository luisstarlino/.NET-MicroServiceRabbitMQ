using MicroRabbit.Analytics.Application.Interfaces;
using MicroRabbit.Analytics.Domain.Interfaces;
using MicroRabbit.Analytics.Domain.Models;
using MicroRabbit.Domain.Core.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroRabbit.Analytics.Application.Services
{
    public class ClientApprovalService : IClientApprovalService
    {
        private readonly IAnalyticsRepository _analyticsRepository;
        private readonly IEventBus _bus;

        public ClientApprovalService(IAnalyticsRepository analyticsRepository, IEventBus bus)
        {
            _analyticsRepository = analyticsRepository;
            _bus = bus;
        }

        public IEnumerable<ClientApproval> GetAllClientApprovals()
        {
            return _analyticsRepository.GetAllClientApprovalProcess();
        }

        public ClientApproval? GetUniqueProcessByClient(int idClient)
        {
            //------------------------------------------------------------------------------------------------
            // GET DATA FROM DB
            //------------------------------------------------------------------------------------------------
            var clientApprovalProcess = _analyticsRepository.GetUniqueByClient(idClient);
            return clientApprovalProcess;
        }
    }
}
