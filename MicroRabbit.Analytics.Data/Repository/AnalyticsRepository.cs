using MicroRabbit.Analytics.Data.Context;
using MicroRabbit.Analytics.Domain.Interfaces;
using MicroRabbit.Analytics.Domain.Models;
using Microsoft.EntityFrameworkCore;
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

        async public Task AddAnalyticsClientLog()
        {
            var today = DateTime.UtcNow.Date;
            var origin = "SYSTEM";

            try
            {
                var countsToday = await _ctx.ClientAnalyticsLogs.FirstOrDefaultAsync(record => record.Date == today);

                if (countsToday != null)
                {
                    countsToday.Count += 1;
                    _ctx.ClientAnalyticsLogs.Update(countsToday);
                }
                else
                {
                    await _ctx.ClientAnalyticsLogs.AddAsync(new ClientAnalyticsLog
                    {
                        Count = 1,
                        CreatedBy = origin,
                        Date = today
                    });
                }

                await _ctx.SaveChangesAsync();

            } catch (Exception ex)
            {
                Console.WriteLine("*******ERROR*******");
                Console.WriteLine(ex);
            }
        }

        async public Task AddAprrovalClientLog()
        {
            var today = DateTime.UtcNow.Date;
            var origin = "SYSTEM";

            try
            {
                var countsToday = await _ctx.ClientApprovalLogs.FirstOrDefaultAsync(record => record.Date == today);

                if (countsToday != null)
                {
                    countsToday.Count += 1;
                    _ctx.ClientApprovalLogs.Update(countsToday);
                }
                else
                {
                    await _ctx.ClientApprovalLogs.AddAsync(new ClientApprovalLog
                    {
                        Count = 1,
                        CreatedBy = origin,
                        Date = today
                    });
                }

                await _ctx.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine("*******ERROR*******");
                Console.WriteLine(ex);
            }
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

        public ClientApproval? GetUniqueByClient(int idClient)
        {
            return _ctx.ClientApprovals.Where(c => c.ClientId.Equals(idClient)).FirstOrDefault() ?? null;
        }
    }
}
