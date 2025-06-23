using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroRabbit.Transfer.Domain.Models
{
    public class TransferCreationLog
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Count { get; set; }
        public string CreatedBy { get; set; }
    }
}
