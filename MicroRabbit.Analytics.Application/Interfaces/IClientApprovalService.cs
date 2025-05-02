using MicroRabbit.Analytics.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroRabbit.Analytics.Application.Interfaces
{
    public interface IClientApprovalService
    {
        IEnumerable<ClientApproval> GetAllClientApprovals();
        ClientApproval? GetUniqueProcessByClient(int idClient);
    }
}
