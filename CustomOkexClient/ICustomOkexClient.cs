using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoExchange.Net.ExchangeInterfaces;
using CustomOkexClient.Objects;
using CustomOkexClient.RestObjects.Common;
using CustomOkexClient.RestObjects.Responses.PublicData;

namespace CustomOkexClient
{
    public interface ICustomOkexClient : IExchangeClient
    {
        IEnumerable<CexPendingDepositDetails> GetCexPendingDeposits();
        Task<IEnumerable<CexPendingDepositDetails>> GetCexPendingDepositsAsync();
        IEnumerable<TradeInstrument> GetInstrumentsWithOpenContracts(
            InstrumentType instrumentType,
            string underlyingForOption = null,
            string instrumentId = null);
        Task<IEnumerable<TradeInstrument>> GetInstrumentsWithOpenContractsAsync(
            InstrumentType instrumentType,
            string underlyingForOption = null,
            string instrumentId = null);
    }
}