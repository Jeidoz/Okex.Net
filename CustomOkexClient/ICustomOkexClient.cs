using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoExchange.Net.ExchangeInterfaces;
using CustomOkexClient.Objects;

namespace CustomOkexClient
{
    public interface ICustomOkexClient : IExchangeClient
    {
        IEnumerable<CexPendingDepositDetails> GetCexPendingDeposits();
        Task<IEnumerable<CexPendingDepositDetails>> GetCexPendingDepositsAsync();
    }
}