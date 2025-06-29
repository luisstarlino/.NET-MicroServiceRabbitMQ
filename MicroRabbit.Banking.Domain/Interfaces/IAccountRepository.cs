﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroRabbit.Banking.Domain.Models;

namespace MicroRabbit.Banking.Domain.Interfaces
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Account>> GetAccounts();
        Task<int> AddAccount(Account account);
        Task<bool> ChangeAccountStatus(int idAcc, bool newStatus);
        Task<decimal?> UpdateAmountAcc(int idAcc, decimal incomeBalace);
        Task AddAccountCreatedLog();
    }
}
