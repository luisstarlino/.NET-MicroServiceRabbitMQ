using MicroRabbit.Transfer.Domain.Models;

namespace MicroRabbit.Transfer.Domain.Interfaces
{
    public interface ITransferRepository
    {
        IEnumerable<TransferLog> GetTransferLogs();
        bool Add(TransferLog transferLog);
        Task AddTransferCreatedLog();
    }
}
