﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroRabbit.Banking.Domain.Models
{
    public class Account
    {
        public int Id { get; set; }
        public AccountType AccountType { get; set; }
        public decimal AccountBalance { get; set; }
        public bool Active { get; set; } = true;

        // Foreign Key Configuration
        public int ClientId { get; set; }
        public Client Client { get; set; } // EF Navigation to this model

        // --- Foreing key configuration
        public ICollection<Balance> Balances { get; set; } // Reverse Navigation (1:N) A account, using many balances

    }
}
