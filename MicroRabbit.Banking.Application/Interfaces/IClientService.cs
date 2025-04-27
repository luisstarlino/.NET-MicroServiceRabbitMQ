using MicroRabbit.Banking.Application.Models;
using MicroRabbit.Banking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroRabbit.Banking.Application.Interfaces
{
    public interface IClientService
    {
        //        IEnumerable<Account> GetAccounts();
        bool AddClient(ClientRequest client);
        IEnumerable<ClientRequest> GetClients();
    }
}
