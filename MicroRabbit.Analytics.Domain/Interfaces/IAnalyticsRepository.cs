using MicroRabbit.Analytics.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroRabbit.Analytics.Domain.Interfaces
{
    public interface IAnalyticsRepository
    {
        IEnumerable<ClientApproval> GetAllClientApprovalProcess();
        bool AddClientApproval(ClientApproval approvalProcess);
        ClientApproval? GetUniqueByClient(int idClient);
        Task AddAprrovalClientLog();
        Task AddAnalyticsClientLog();
    }
}
