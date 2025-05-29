using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroRabbit.Banking.Domain.Models
{
    public class Balance
    {
        [Key]
        public Guid Id { get; set; } = new Guid();
        public decimal Amount { get; set; }
        public decimal Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Foreign Key Configuration
        public int AccountId { get; set; }
        public Account Account { get; set; } // EF Navigation to this model    
    }
}
