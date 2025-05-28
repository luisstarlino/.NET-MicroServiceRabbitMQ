using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroRabbit.Analytics.Domain.Interfaces
{
    public interface IBankingRepository
    {
        Task<bool> UpdateClientStatus(int idClient, bool status);
    }
}
