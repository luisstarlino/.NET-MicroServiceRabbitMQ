using MicroRabbit.Banking.Application.Interfaces;
using MicroRabbit.Banking.Application.Models;
using MicroRabbit.Banking.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroRabbit.Banking.Application.Services
{
    public class DashboardService : IDashboardService
    {

        private readonly IClientRepository _clientRepository;

        public DashboardService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        async public Task<List<DashboardCardResponse>> GetDashboardCards()
        {
            var cards = new List<DashboardCardResponse>();
            try
            {
                // ------------------------------------------------------------------------------------------------
                // Card 1. NEW CLIENTS --> COMPARE THIS MONTH WITH THE LAST ONE
                //------------------------------------------------------------------------------------------------
                var cardNewClients = await CompareCurrentAndPreviousMonth();
                cards.Add(cardNewClients);

            }
            catch (Exception ex)
            {

            }
            return cards;
        }

        async private Task<DashboardCardResponse> CompareCurrentAndPreviousMonth()
        {

            // ------------------------------------------------------------------------------------------------
            // CONST's
            //------------------------------------------------------------------------------------------------
            DateTime today = DateTime.Today;
            DateTime firstDayNextMonth = new DateTime(today.Year, today.Month + 1, 1);

            try
            {
                // --- This month
                DateTime startOfCurrent = firstDayNextMonth.AddMonths(-1);
                DateTime endOfCurrent = firstDayNextMonth.AddDays(-1);

                // --- Last month
                DateTime startOfLast = new DateTime(today.Year, today.Month - 1, 1);
                DateTime endOfLast = startOfCurrent.AddDays(-1);

                // -------------------------------------------------------------------
                // --- Count by range
                // -------------------------------------------------------------------
                int totalOfCUrrent = await _clientRepository.GetCountByInterval(startOfCurrent, endOfCurrent);
                int totalOfLast = await _clientRepository.GetCountByInterval(startOfLast, endOfLast);



                // -------------------------------------------------------------------
                // --- Percent
                // -------------------------------------------------------------------
                double percentChange = totalOfLast != 0  ? ((totalOfCUrrent - totalOfLast) / (double)totalOfLast) * 100 : 100.00;

                var fullCard = new DashboardCardResponse
                {
                    Title = "New Clients",
                    Subtitle = "This Month",
                    Description = percentChange < 0 ? $"Down {percentChange:F2}% this perid" : $"Up {percentChange:F2}% this perid",
                    MainValue = $"{totalOfCUrrent}",
                    PercentHit = $"{percentChange:F2}%"
                };



                return fullCard;
            }
            catch
            {
                return new DashboardCardResponse();
            }

        }
    }
}
