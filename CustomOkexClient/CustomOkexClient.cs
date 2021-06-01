using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CryptoExchange.Net.ExchangeInterfaces;
using CryptoExchange.Net.Objects;
using CustomCexWrapper.Helpers;
using CustomCexWrapper.Objects;
using CustomCexWrapper.Objects.Config;
using CustomCexWrapper.RestObjects.Common;
using CustomCexWrapper.RestObjects.Responses.PublicData;

namespace CustomCexWrapper
{
    public class CustomOkexClient : ICustomOkexClient
    {
        private readonly OkexRestClient _restClient;

        public CustomOkexClient(OkexApiCredentials okexCredentials, bool isDemo = false)
        {
            _restClient = new OkexRestClient(okexCredentials, isDemo);
        }

        public string GetSymbolName(string baseAsset, string quoteAsset)
        {
            throw new NotImplementedException();
        }

        public Task<WebCallResult<IEnumerable<ICommonSymbol>>> GetSymbolsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<WebCallResult<IEnumerable<ICommonTicker>>> GetTickersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<WebCallResult<ICommonTicker>> GetTickerAsync(string symbol)
        {
            throw new NotImplementedException();
        }

        public Task<WebCallResult<IEnumerable<ICommonKline>>> GetKlinesAsync(string symbol, TimeSpan timespan, DateTime? startTime = null, DateTime? endTime = null, int? limit = null)
        {
            throw new NotImplementedException();
        }

        public Task<WebCallResult<ICommonOrderBook>> GetOrderBookAsync(string symbol)
        {
            throw new NotImplementedException();
        }

        public Task<WebCallResult<IEnumerable<ICommonRecentTrade>>> GetRecentTradesAsync(string symbol)
        {
            throw new NotImplementedException();
        }

        public Task<WebCallResult<ICommonOrderId>> PlaceOrderAsync(string symbol, IExchangeClient.OrderSide side, IExchangeClient.OrderType type, decimal quantity, decimal? price = null, string accountId = null)
        {
            throw new NotImplementedException();
        }

        public Task<WebCallResult<ICommonOrder>> GetOrderAsync(string orderId, string symbol = null)
        {
            throw new NotImplementedException();
        }

        public Task<WebCallResult<IEnumerable<ICommonTrade>>> GetTradesAsync(string orderId, string symbol = null)
        {
            throw new NotImplementedException();
        }

        public Task<WebCallResult<IEnumerable<ICommonOrder>>> GetOpenOrdersAsync(string symbol = null)
        {
            throw new NotImplementedException();
        }

        public Task<WebCallResult<IEnumerable<ICommonOrder>>> GetClosedOrdersAsync(string symbol = null)
        {
            throw new NotImplementedException();
        }

        public Task<WebCallResult<ICommonOrderId>> CancelOrderAsync(string orderId, string symbol = null)
        {
            throw new NotImplementedException();
        }

        public Task<WebCallResult<IEnumerable<ICommonBalance>>> GetBalancesAsync(string accountId = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CexPendingDepositDetails> GetCexPendingDeposits()
        {
            return GetCexPendingDepositsAsync().Result;
        }

        public async Task<IEnumerable<CexPendingDepositDetails>> GetCexPendingDepositsAsync()
        {
            var result = await _restClient.Funding_GetDepositHistory();
            if (!result.Success)
            {
                ThrowExceptionForFailedRequest(result.Error, nameof(GetCexPendingDepositsAsync));
            }

            return result.Data
                .Where(r => r.State != DepositState.DepositSuccessful)
                .Select(r => new CexPendingDepositDetails
                {
                    Amount = r.Amount,
                    Tx = r.TxId,
                    AssetName = r.Currency
                })
                .ToArray();
        }

        public IEnumerable<TradeInstrument> GetInstrumentsWithOpenContracts(
            InstrumentType instrumentType,
            string underlyingForOption = null,
            string instrumentId = null)
        {
            return GetInstrumentsWithOpenContractsAsync(instrumentType, underlyingForOption, instrumentId).Result;
        }

        public async Task<IEnumerable<TradeInstrument>> GetInstrumentsWithOpenContractsAsync(
            InstrumentType instrumentType,
            string underlyingForOption = null,
            string instrumentId = null)
        {
            instrumentId.ValidateSymbol();
            var result = await _restClient.PublicData_GetInstruments(instrumentType, underlyingForOption, instrumentId);
            if (!result.Success)
            {
                ThrowExceptionForFailedRequest(result.Error, nameof(GetInstrumentsWithOpenContractsAsync));
            }

            return result.Data;
        }

        public IDictionary<string, OrderBook> GetFuturesUsdtOrderBooks()
        {
            return GetFuturesUsdtOrderBooksAsync().Result;
        }

        public async Task<IDictionary<string, OrderBook>> GetFuturesUsdtOrderBooksAsync()
        {
            var symbols = await _restClient.PublicData_GetInstruments(InstrumentType.Futures);
            var usdtSymbols = symbols.Data
                .Where(s => s.SettlementAndMarginCurrency.Equals("USDT", StringComparison.InvariantCultureIgnoreCase))
                .Select(s => s.Id)
                .ToArray();

            var numProcs = Environment.ProcessorCount;
            var concurrencyLevel = numProcs * 2;
            var orderBooks = new ConcurrentDictionary<string, OrderBook>(concurrencyLevel, usdtSymbols.Length);

            Parallel.ForEach(usdtSymbols, symbol =>
            {
                var orderBook = _restClient.MarketData_GetFuturesOrderBook(symbol).Result;
                if (orderBook.Success)
                {
                    orderBooks.TryAdd(symbol, orderBook.Data);
                }
            });

            return orderBooks
                .OrderBy(kvp => kvp.Key)
                .ThenBy(kvp => kvp.Value)
                .ToDictionary(kvp => kvp.Key, t => t.Value);
        }

        public CustomFutureOrder FuturesPlaceOrderByMarket(string symbol, CustomOrderSide side, decimal quantity)
        {
            return FuturesPlaceOrderByMarketAsync(symbol, side, quantity).Result;
        }

        public async Task<CustomFutureOrder> FuturesPlaceOrderByMarketAsync(string symbol, CustomOrderSide side, decimal quantity)
        {
            symbol.ValidateSymbol();
            var placeResponse = await _restClient.MarketData_Futures_PlaceOrderByMarket(symbol, side, quantity);
            if (!placeResponse.Success)
            {
                ThrowExceptionForFailedRequest(placeResponse.Error, nameof(FuturesPlaceOrderByMarketAsync));
            }

            var orderDetailsResponse = await _restClient.MarketData_GetOrderDetails(symbol, placeResponse.Data.OrderId);
            if (!orderDetailsResponse.Success)
            {
                ThrowExceptionForFailedRequest(orderDetailsResponse.Error, nameof(FuturesPlaceOrderByMarketAsync));
            }

            return new CustomFutureOrder
            {
                Price = orderDetailsResponse.Data.LastFilledPrice,
                Status = orderDetailsResponse.Data.State.ToString(),
                Symbol = orderDetailsResponse.Data.Symbol,
                AvgPrice = orderDetailsResponse.Data.AverageFilledPrice,
                CreatedTime = orderDetailsResponse.Data.CreationTime,
                ExecutedQuantity = orderDetailsResponse.Data.ExecutedQuantity,
                OrderId = long.Parse(orderDetailsResponse.Data.OrderId)
            };
        }

        private void ThrowExceptionForFailedRequest(Error error, string methodName)
        {
            var errorDecription = $"Request {methodName} failed.\n";
            if (error != null)
            {
                if (error.Code != null)
                {
                    errorDecription += $"Error Code: {error.Code}\n";
                }

                if (string.IsNullOrEmpty(error.Message))
                {
                    errorDecription += $"Error Message: {error.Message}\n";
                }
            }
            throw new HttpRequestException(errorDecription);
        }
    }
}