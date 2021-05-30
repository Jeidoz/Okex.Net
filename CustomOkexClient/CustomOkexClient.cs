using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CryptoExchange.Net.ExchangeInterfaces;
using CryptoExchange.Net.Objects;
using CustomOkexClient.Converters;
using CustomOkexClient.Objects;
using CustomOkexClient.Objects.Config;
using CustomOkexClient.RestObjects.Common;
using CustomOkexClient.RestObjects.Responses.Funding;
using Newtonsoft.Json;

namespace CustomOkexClient
{
    public class CustomOkexClient : ICustomOkexClient
    {
        private OkexRestClient _restClient;

        public CustomOkexClient(OkexApiCredentials okexCredentials)
        {
            _restClient = new OkexRestClient(okexCredentials);
        }

        private void ThrowExceptionForFailedRequest(Error error, string methodName)
        {
            var errorDecription = $"Request {methodName} failed.\n";
            if (error is not null)
            {
                if (error.Code is not null)
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

        // public async Task<IEnumerable<CexPendingDepositDetails>> GetCexPendingDepositsAsync()
        // {
        //     var fundingDepositHistory = await Funding_GetDepositHistory_Async();
        //     var spotBalances = await Spot_GetAllBalances_Async();
        //     // var marginBalances = await Margin_GetAllBalances_Async();
        //     // var futuresBalances = await Futures_GetBalances_Async();
        //     // var optionsBalances = await Options_GetBalances_Async("BTC-USD");
        //     // var swapBalances = await Swap_GetBalances_Async();
        //
        //     var depositDetails = new List<CexPendingDepositDetails>();
        //     depositDetails.AddRange(await GetPendingFundingDepositsAsync(fundingDepositHistory));
        //     depositDetails.AddRange(await GetPendingSpotDepositsAsync(spotBalances));
        //
        //     return depositDetails;
        // }
        //
        // private async Task<IEnumerable<CexPendingDepositDetails>> GetPendingFundingDepositsAsync(
        //     WebCallResult<IEnumerable<OkexFundingDepositDetails>> webCallResponse)
        // {
        //     return await Task.Run(() =>
        //     {
        //         if (!webCallResponse.Success)
        //         {
        //             ThrowExceptionForFailedRequest(webCallResponse.Error, nameof(GetPendingFundingDepositsAsync));
        //         }
        //
        //         var depositsHistories = webCallResponse.Data;
        //
        //         return depositsHistories
        //             .Where(record => record.Status != OkexFundingDepositStatus.DepositSuccessful)
        //             .Select(r => new CexPendingDepositDetails
        //             {
        //                 Amount = r.Amount,
        //                 AssetName = r.Currency,
        //                 Tx = r.TxId
        //             })
        //             .ToList();
        //     });
        // }
        //
        // private async Task<IEnumerable<CexPendingDepositDetails>> GetPendingSpotDepositsAsync(
        //     WebCallResult<IEnumerable<OkexSpotBalance>> webCallResponse)
        // {
        //     return await Task.Run(() =>
        //     {
        //         if (!webCallResponse.Success)
        //         {
        //             ThrowExceptionForFailedRequest(webCallResponse.Error, nameof(GetPendingSpotDepositsAsync));
        //         }
        //         var balances = webCallResponse.Data;
        //
        //         return balances
        //             .Where(r => r.Frozen > 0)
        //             .Select(r => new CexPendingDepositDetails
        //             {
        //                 Amount = r.Frozen,
        //                 AssetName = r.Currency,
        //                 Tx = r.Id
        //             })
        //             .ToList();
        //     });
        // }
        //
        // private async Task<IEnumerable<CexPendingDepositDetails>> GetPendingMarginDepositsAsync(
        //     WebCallResult<IEnumerable<OkexMarginBalance>> webCallResponse)
        // {
        //     return await Task.Run(() =>
        //     {
        //         var balances = webCallResponse.Success ? webCallResponse.Data : throw new Exception();
        //
        //         var details = new List<CexPendingDepositDetails>();
        //         foreach (var balance in balances)
        //         {
        //             details.AddRange(balance.Currencies
        //                 .Select(currencyPair => new CexPendingDepositDetails
        //                 {
        //                     Amount = currencyPair.Value.Balance - currencyPair.Value.Available,
        //                     AssetName = currencyPair.Key,
        //                     Tx = "none"
        //                 }));
        //         }
        //         
        //         return details;
        //     });
        // }
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

        public Task<WebCallResult<ICommonOrderId>> PlaceOrderAsync(string symbol, IExchangeClient.OrderSide side, IExchangeClient.OrderType type, decimal quantity, decimal? price = null, string? accountId = null)
        {
            throw new NotImplementedException();
        }

        public Task<WebCallResult<ICommonOrder>> GetOrderAsync(string orderId, string? symbol = null)
        {
            throw new NotImplementedException();
        }

        public Task<WebCallResult<IEnumerable<ICommonTrade>>> GetTradesAsync(string orderId, string? symbol = null)
        {
            throw new NotImplementedException();
        }

        public Task<WebCallResult<IEnumerable<ICommonOrder>>> GetOpenOrdersAsync(string? symbol = null)
        {
            throw new NotImplementedException();
        }

        public Task<WebCallResult<IEnumerable<ICommonOrder>>> GetClosedOrdersAsync(string? symbol = null)
        {
            throw new NotImplementedException();
        }

        public Task<WebCallResult<ICommonOrderId>> CancelOrderAsync(string orderId, string? symbol = null)
        {
            throw new NotImplementedException();
        }

        public Task<WebCallResult<IEnumerable<ICommonBalance>>> GetBalancesAsync(string? accountId = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CexPendingDepositDetails> GetCexPendingDeposits()
        {
            return GetCexPendingDepositsAsync().Result;
        }
        
        public async Task<IEnumerable<CexPendingDepositDetails>> GetCexPendingDepositsAsync()
        {
            var result = await _restClient.FundingGetDepositHistory();
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
    }
}