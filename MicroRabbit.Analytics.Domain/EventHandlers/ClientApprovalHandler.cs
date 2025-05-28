using MicroRabbit.Analytics.Domain.Events;
using MicroRabbit.Analytics.Domain.Helpers;
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
        private readonly IBankingRepository _bankingRepository;
        private const string DEFAULT_PROCESS_OPERATOR = "RABBIT_BOT";
        private const string DEFAULT_COMPLEMENT = "Please, send an email to approval@baking.com with the request corrections and we will review your registration";

        public ClientApprovalHandler(IAnalyticsRepository analyticsRepository, IBankingRepository bankingRepository)
        {
            _analyticsRepository = analyticsRepository;
            _bankingRepository = bankingRepository;
        }

        async public Task Handle(ClientApprovalEvent @event)
        {
            //------------------------------------------------------------------------------------------------
            // CONST
            //------------------------------------------------------------------------------------------------
            bool isValid = false;
            var reason = String.Empty;

            var validator = new ClientApprovalValidator();

            try
            {
                //------------------------------------------------------------------------------------------------
                // Check roles
                //------------------------------------------------------------------------------------------------
                var validationResult = validator.Validate(@event);
                if (!validationResult.IsValid)
                {
                    reason = string.Join(" ", validationResult.Errors.Select(e => e.ErrorMessage)) + $" {DEFAULT_COMPLEMENT}";
                }
                else
                {
                    isValid = true;

                    // --- UPDATE TO TRUE IN MAIN DB
                    await _bankingRepository.UpdateClientStatus(@event.Id, true);

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

            return;
        }
    }
}
