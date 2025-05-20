using FluentValidation;
using MicroRabbit.Analytics.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroRabbit.Analytics.Domain.Helpers
{
    public class ClientApprovalValidator : AbstractValidator<ClientApprovalEvent>
    {
        public ClientApprovalValidator()
        {
            //------------------------------------------------------------------------------------------------
            // ROLE 1: Invalid mail (we don't accept mails coming from yahoo )
            //------------------------------------------------------------------------------------------------
            RuleFor(x => x.Mail)
                .Must(mail => !mail.Contains("yahoo", StringComparison.InvariantCulture))
                .WithMessage("Unfortunatly, we don't accept emails registered with yahoo");

            //------------------------------------------------------------------------------------------------
            // ROLE 2: Invalid phone (Pattern DDI+DDD+PHONE | 5531999999999)
            //------------------------------------------------------------------------------------------------
            RuleFor(x => x.Phone)
               .Matches(@"^55\d{10,11}$")
               .WithMessage("Invalid phone number, please use the format DDI+DDD+Phone (e.g., 5531999999999).");
        }
    }
}
