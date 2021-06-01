using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoExchange.Net.ExchangeInterfaces;
using CustomCexWrapper.Objects;
using CustomCexWrapper.RestObjects.Common;
using CustomCexWrapper.RestObjects.Responses.PublicData;

namespace CustomCexWrapper
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
        IDictionary<string, OrderBook> GetFuturesUsdtOrderBooks();
        Task<IDictionary<string, OrderBook>> GetFuturesUsdtOrderBooksAsync();
        CustomFutureOrder FuturesPlaceOrderByMarket(string symbol, CustomOrderSide side, decimal quantity);
        Task<CustomFutureOrder> FuturesPlaceOrderByMarketAsync(string symbol, CustomOrderSide side, decimal quantity);
    }
}