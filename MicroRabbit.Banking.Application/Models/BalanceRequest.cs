using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroRabbit.Banking.Application.Models
{
    public class BalanceRequest
    {
        public decimal Amount { get; set; }
        public int AccountId { get; set; }
        public decimal Description { get; set; }
    }
}
