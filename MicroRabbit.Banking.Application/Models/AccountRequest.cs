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
        public int AccountType { get; set; }
        public decimal AccountBalance { get; set; }
        public int ClientId { get; set; }

    }

    public class AccountStatusRequest
    {
        public bool NewStatus { get; set; }

    }
}
