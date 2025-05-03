using MicroRabbit.Analytics.Domain.Events;
using MicroRabbit.Analytics.Domain.Interfaces;
using MicroRabbit.Domain.Core.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MicroRabbit.Analytics.Domain.EventHandlers
{
    public class ClientApprovalHandler : IEventHandler<ClientApprovalEvent>
    {
        private readonly IAnalyticsRepository _analyticsRepository;
        private const string DEFAULT_PROCESS_OPERATOR = "RABBIT_BOT";
        private const string DEFAULT_COMPLEMENT = "Please, send an email to approval@baking.com with the request corrections and we will review your registration";

        public ClientApprovalHandler(IAnalyticsRepository analyticsRepository)
        {
            _analyticsRepository = analyticsRepository;
        }

        public Task Handle(ClientApprovalEvent @event)
        {
            //------------------------------------------------------------------------------------------------
            // CONST
            //------------------------------------------------------------------------------------------------
            bool isValid = false;
            var reason = String.Empty;

            try
            {
                //------------------------------------------------------------------------------------------------
                // ROLE 1: Invalid mail (we don't accept mails coming from yahoo )
                //------------------------------------------------------------------------------------------------
                if (@event.Mail.Contains("yahoo", StringComparison.CurrentCultureIgnoreCase))
                {
                    reason = $"Unfortunately, we don't accept emails registered with yahoo. {DEFAULT_COMPLEMENT}";
                    
                }
                //------------------------------------------------------------------------------------------------
                // ROLE 2: Invalid phone (Patter DDI+DDD+PHONE | 5531999999999)
                //------------------------------------------------------------------------------------------------
                else if (Regex.IsMatch(@event.Phone, @"^55\d{10,11}$"))
                {
                    reason = $"Invalid phone number, please review the acceptance rules for this field. Unfortunately, we don't accept models like this. {DEFAULT_COMPLEMENT}";
                } else
                {
                    // --- All good
                    isValid = true;
                }

                //------------------------------------------------------------------------------------------------
                // TODO: CHECK EMAIL IN DB | CHECK EMAIL + FULLNAME IN THE DB
                //------------------------------------------------------------------------------------------------

            }
            catch (Exception e)
            {
                reason = "An error occurred during your approval process. Please, contact the mail approva@baking.com to continue with your process";
            }

            //------------------------------------------------------------------------------------------------
            // SAVE ANALYTICS RESULTS FOR THIS PROCESS
            //------------------------------------------------------------------------------------------------
            _analyticsRepository.AddClientApproval(new Models.ClientApproval
            {
                ApprovalStatus = isValid,
                ClientId = @event.Id,
                ProcessedBy = DEFAULT_PROCESS_OPERATOR,
                CreatedDate = DateTime.UtcNow,
                Reason = reason
            });

            return Task.CompletedTask;
        }
    }
}
