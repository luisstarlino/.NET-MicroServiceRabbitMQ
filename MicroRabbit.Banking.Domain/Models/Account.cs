using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroRabbit.Banking.Domain.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string AccountType { get; set; }
        public decimal AccountBalance { get; set; }

        // Foreign Key Configuration
        public int ClientId { get; set; }
        public Client Client { get; set; } // EF Navigation to this model

    }
}
