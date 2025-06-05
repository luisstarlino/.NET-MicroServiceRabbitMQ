using MicroRabbit.Banking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroRabbit.Banking.Domain.Interfaces
{
    public interface IBalanceRepository
    {
        Task<IEnumerable<Balance>> GetAllBalances();
        Task<IEnumerable<Balance>> GetAllBalancesByAccount(int accountId);
        Task<Balance?> GetUniqueBalance(Guid guidBalance);
        Task<Guid> AddBalance(Balance balance);
    }
}
