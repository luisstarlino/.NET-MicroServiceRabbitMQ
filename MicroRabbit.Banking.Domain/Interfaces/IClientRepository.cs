using MicroRabbit.Banking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroRabbit.Banking.Domain.Interfaces
{
    public interface IClientRepository
    {
        IEnumerable<Client> GetAllClients();
        Client? GetById(int clientId);
        int Add(Client client);
        Task AddCreatedLog();
        Task<List<ClientCreationLog>> GetFullDashboardClientCreation();
        Task<int> GetCountByInterval(DateTime startRange, DateTime endRange);
    }
}
