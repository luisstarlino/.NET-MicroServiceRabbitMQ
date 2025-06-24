using MicroRabbit.Banking.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroRabbit.Banking.Application.Interfaces
{
    public interface IDashboardService
    {
        Task<List<DashboardCardResponse>> GetDashboardCards();
    }
}
