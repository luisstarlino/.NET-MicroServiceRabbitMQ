using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroRabbit.Banking.Domain.Commands
{
    public class CreateClientApprovalCommand : ClientApprovalCommand
    {
        public CreateClientApprovalCommand(int id, string firstName, string lastName, string mail, string phone)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            Mail = mail;
        }
    }
}
