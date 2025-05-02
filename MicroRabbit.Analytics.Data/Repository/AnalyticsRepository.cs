using MicroRabbit.Analytics.Data.Context;
using MicroRabbit.Analytics.Domain.Interfaces;
using MicroRabbit.Analytics.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroRabbit.Analytics.Data.Repository
{
    public class AnalyticsRepository : IAnalyticsRepository
    {
        private AnalyticsDbContext _ctx;

        public AnalyticsRepository(AnalyticsDbContext ctx)
        {
            _ctx = ctx;
        }

        public bool AddClientApproval(ClientApproval approvalProcess)
        {
            _ctx.Add(approvalProcess);
            _ctx.SaveChanges();
            return true;
        }

        public IEnumerable<ClientApproval> GetAllClientApprovalProcess()
        {
            return _ctx.ClientApprovals;
        }
    }
}
