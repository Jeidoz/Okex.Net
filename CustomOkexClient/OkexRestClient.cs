using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using CustomCexWrapper.Helpers;
using CustomCexWrapper.Objects.Config;
using CustomCexWrapper.RestObjects.Common;
using CustomCexWrapper.RestObjects.Responses.Funding;
using CustomCexWrapper.RestObjects.Responses.PublicData;
using Newtonsoft.Json;

namespace CustomCexWrapper
{
    public sealed class OkexRestClient
    {
        private const string BaseOkexApiUrl = "https://www.okex.com/";

        private bool IsDemoAccount { get; set; }

        private readonly OkexApiCredentials _apiCredentials;
        private readonly HttpClient _httpClient;

        public OkexRestClient(OkexApiCredentials credentials, bool isDemo = false)
        {
            IsDemoAccount = isDemo;
            _apiCredentials = credentials;
            _httpClient = new HttpClient {BaseAddress = new Uri(BaseOkexApiUrl)};
            SetUpDefaultHeaders();
        }

        private void SetUpDefaultHeaders()
        {
            _httpClient.DefaultRequestHeaders.Add("OK-ACCESS-KEY", _apiCredentials.ApiKey);
            if (!string.IsNullOrEmpty(_apiCredentials.Passphrase))
            {
                _httpClient.DefaultRequestHeaders.Add("OK-ACCESS-PASSPHRASE", _apiCredentials.Passphrase);
            }

            if (IsDemoAccount)
            {
                _httpClient.DefaultRequestHeaders.Add("X-SIMULATED-TRADING", "1");
            }
        }

        private async void SetUpSignatureHeader(HttpRequestMessage request)
        {
            var timestamp = (DateTime.UtcNow.ToUnixTimeMilliSeconds() / 1000.0m).ToString(CultureInfo.InvariantCulture);
            request.Headers.Add("OK-ACCESS-TIMESTAMP", timestamp);

            var method = request.Method.Method.ToUpper();
            var url = '/' + request.RequestUri?.ToString().Trim('?');
            var unencryptedSignature = $"{timestamp}{method}{url}";
            if (request.Content != null)
            {
                var body = await request.Content.ReadAsStringAsync();
                unencryptedSignature += body;
            }

            var signatureBytes = Encoding.UTF8.GetBytes(unencryptedSignature);
            var secretKeyBytes = Encoding.UTF8.GetBytes(_apiCredentials.ApiSecret);

            byte[] hmacSha256Signature;
            using (var hmacSha256Encoder = new HMACSHA256(secretKeyBytes))
            {
                hmacSha256Signature = hmacSha256Encoder.ComputeHash(signatureBytes);
            }
            var finalSignature = Convert.ToBase64String(hmacSha256Signature);
            request.Headers.Add("OK-ACCESS-SIGN", finalSignature);
        }

        private async Task<HttpResponseMessage> SendGetRequestAsync(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            SetUpSignatureHeader(request);
            return await _httpClient.SendAsync(request);
        }

        public async Task<WebCallResult<IEnumerable<DepositDetails>>> Funding_GetDepositHistory(
            string currency = null,
            DepositState? state = null,
            DateTime? after = null,
            DateTime? before = null,
            int limit = 100)
        {
            var url = new StringBuilder("api/v5/asset/deposit-history?");

            if (!string.IsNullOrEmpty(currency)) url.Append($"ccy={currency}&");
            if (state.HasValue) url.Append($"state={(int) state.Value}&");
            if (after.HasValue) url.Append($"after={after.Value.ToUnixTimeMilliSeconds()}&");
            if (before.HasValue) url.Append($"after={before.Value.ToUnixTimeMilliSeconds()}&");
            url.Append($"limit={limit}");

            var response = await SendGetRequestAsync(url.ToString());
            var json = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                //throw new HttpRequestException($"Request failed:\nStatus Code:{response.StatusCode}\n{json}");
                var errorResponse = JsonConvert.DeserializeObject<BaseResponse<DepositDetails>>(json);
                return WebCallResult<IEnumerable<DepositDetails>>
                    .CreateErrorResult(
                        response.StatusCode,
                        response.Headers,
                        new WebError(errorResponse.Code, errorResponse.Message));
            }

            var baseResponse = JsonConvert.DeserializeObject<BaseResponse<DepositDetails>>(json);
            return new WebCallResult<IEnumerable<DepositDetails>>(
                response.StatusCode,
                response.Headers,
                baseResponse.Data,
                null);
        }

        public async Task<WebCallResult<IEnumerable<TradeInstrument>>> PublicData_GetInstruments(
            InstrumentType type,
            string underlyingForOption = null,
            string instrumentId = null)
        {
            if (type == InstrumentType.OPTION && string.IsNullOrEmpty(underlyingForOption))
            {
                return WebCallResult<IEnumerable<TradeInstrument>>.CreateErrorResult(
                    null,
                    null,
                    new ArgumentError("Underlying argument is required for OPTION trade."));
            }

            var url = new StringBuilder("api/v5/public/instruments?");

            url.Append($"instType={type.ToString()}");
            if (!string.IsNullOrEmpty(underlyingForOption)) url.Append($"&uly={underlyingForOption}");
            if (!string.IsNullOrEmpty(instrumentId)) url.Append($"&instId={instrumentId}");

            var response = await SendGetRequestAsync(url.ToString());
            var json = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = JsonConvert.DeserializeObject<BaseResponse<TradeInstrument>>(json);
                return WebCallResult<IEnumerable<TradeInstrument>>
                    .CreateErrorResult(
                        response.StatusCode,
                        response.Headers,
                        new WebError(errorResponse.Code, errorResponse.Message));
            }

            var baseResponse = JsonConvert.DeserializeObject<BaseResponse<TradeInstrument>>(json);
            return new WebCallResult<IEnumerable<TradeInstrument>>(
                response.StatusCode,
                response.Headers,
                baseResponse.Data,
                null);
        }

        public async Task<WebCallResult<OrderBook>> MarketData_GetFuturesOrderBook(string instrumentId, int depth = 20)
        {
            var url = $"api/v5/market/books?instId={instrumentId}&sz={depth}";

            var response = await SendGetRequestAsync(url);
            var json = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = JsonConvert.DeserializeObject<BaseResponse<OrderBook>>(json);
                return WebCallResult<OrderBook>
                    .CreateErrorResult(
                        response.StatusCode,
                        response.Headers,
                        new WebError(errorResponse.Code, errorResponse.Message));
            }

            var baseResponse = JsonConvert.DeserializeObject<BaseResponse<OrderBook>>(json);
            return new WebCallResult<OrderBook>(
                response.StatusCode,
                response.Headers,
                baseResponse.Data.FirstOrDefault(),
                null);
        }
    }
}