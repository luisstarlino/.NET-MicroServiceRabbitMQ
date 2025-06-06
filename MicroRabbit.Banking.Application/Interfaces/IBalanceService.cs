﻿using MicroRabbit.Banking.Application.Models;
using MicroRabbit.Banking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroRabbit.Banking.Application.Interfaces
{
    public interface IBalanceService
    {
        Task<Guid> AddBalance(BalanceRequest balance, int accountId);
        Task<IEnumerable<Balance?>> ListAllBalancesByAcc(int accountId);
    }
}
