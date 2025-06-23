using MicroRabbit.Transfer.Data.Context;
using MicroRabbit.Transfer.Domain.Interfaces;
using MicroRabbit.Transfer.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroRabbit.Transfer.Data.Repository
{
    public class TransferRepository : ITransferRepository
    {
        private TransferDbContext _ctx;

        public TransferRepository(TransferDbContext ctx)
        {
            _ctx = ctx;
        }

        public bool Add(TransferLog transferLog)
        {
            _ctx.Add(transferLog);
            _ctx.SaveChanges();
            return true;
        }

        async public Task AddTransferCreatedLog()
        {
            var today = DateTime.UtcNow.Date;
            var origin = "SYSTEM";

            try
            {
                var countsToday = await _ctx.TransferCreationLogs.FirstOrDefaultAsync(record => record.Date == today && record.CreatedBy == origin);

                if (countsToday != null)
                {
                    countsToday.Count += 1;
                    _ctx.TransferCreationLogs.Update(countsToday);
                }
                else
                {
                    await _ctx.TransferCreationLogs.AddAsync(new TransferCreationLog
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

        public IEnumerable<TransferLog> GetTransferLogs()
        {
            return _ctx.TransferLogs;
        }
    }
}
