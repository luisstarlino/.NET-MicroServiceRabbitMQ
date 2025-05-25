using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroRabbit.Banking.Application.Models
{
    public class AccountRequest
    {
        public string AccountType { get; set; }
        public decimal AccountBalance { get; set; }
        public int ClientId { get; set; }

    }
}
