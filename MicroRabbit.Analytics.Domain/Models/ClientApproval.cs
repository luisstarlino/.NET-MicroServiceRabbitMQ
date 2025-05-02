using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroRabbit.Analytics.Domain.Models
{
    public class ClientApproval
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public bool ApprovalStatus { get; set; }
        public string Reason { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ProcessedBy { get; set; }
    }
}
