using MicroRabbit.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroRabbit.Banking.Domain.Events
{
    public class ClientApprovalEvent : Event
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mail { get; set; }
        public string Phone { get; set; }

        public ClientApprovalEvent(int id, string fName, string lName, string mail, string phone)
        {
            Id = id;
            FirstName = fName;
            LastName = lName;
            Mail = mail;
            Phone = phone;
        }
    }
}
