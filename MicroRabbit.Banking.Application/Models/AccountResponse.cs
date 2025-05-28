using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroRabbit.Banking.Application.Models
{
    public class AccountResponse
    {
        public bool Success { get; set; } = true;
        public int IdAccount { get; set; }
        public string ErroMessage { get; set; }
    }
}
