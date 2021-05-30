using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using CustomOkexClient.Converters;
using CustomOkexClient.Helpers;
using CustomOkexClient.Objects.Config;
using CustomOkexClient.RestObjects.Common;
using CustomOkexClient.RestObjects.Responses.Funding;
using Newtonsoft.Json;

namespace CustomOkexClient
{
    public sealed class OkexRestClient
    {
        private const string BaseOkexApiUrl = "https://www.okex.com/";

        private readonly OkexApiCredentials _apiCredentials;
        private readonly JsonSerializerSettings _serializerSettings;
        private readonly HttpClient _httpClient;

        public OkexRestClient(OkexApiCredentials credentials)
        {
            _apiCredentials = credentials;
            _serializerSettings = new JsonSerializerSettings
            {
                Converters =
                {
                    new FormatNumbersAsTextConverter()
                }
            };
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(BaseOkexApiUrl);
            SetUpDefaultHeaders();
        }

        private void SetUpDefaultHeaders()
        {
            _httpClient.DefaultRequestHeaders.Add("OK-ACCESS-KEY", _apiCredentials.ApiKey);
            if (!string.IsNullOrEmpty(_apiCredentials.Passphrase))
            {
                _httpClient.DefaultRequestHeaders.Add("OK-ACCESS-PASSPHRASE", _apiCredentials.Passphrase);
            }
        }

        private async void SetUpSignatureHeader(HttpRequestMessage request)
        {
            // var now = DateTime.UtcNow + TimeSpan.FromHours(3);
            // var timestamp = $"{now:yyyy-MM-dd}T{now:hh:mm:ss.fff}Z";
            var timestamp = (DateTime.UtcNow.ToUnixTimeMilliSeconds() / 1000.0m).ToString(CultureInfo.InvariantCulture);
            request.Headers.Add("OK-ACCESS-TIMESTAMP", timestamp);
            
            var method = request.Method.Method.ToUpper();
            var url = '/' + request.RequestUri?.ToString().Trim('?');
            var unencryptedSignature = $"{timestamp}{method}{url}";
            if (request.Content is not null)
            {
                var body = await request.Content.ReadAsStringAsync();
                unencryptedSignature += body;
            }
            
            var signatureBytes = Encoding.UTF8.GetBytes(unencryptedSignature);
            var secretKeyBytes = Encoding.UTF8.GetBytes(_apiCredentials.ApiSecret);
            using var hmacSha256Encoder = new HMACSHA256(secretKeyBytes);
            var hmacSha256Signature = hmacSha256Encoder.ComputeHash(signatureBytes);
            
            var finalSignature = Convert.ToBase64String(hmacSha256Signature);
            request.Headers.Add("OK-ACCESS-SIGN", finalSignature);
        }

        public async Task<WebCallResult<IEnumerable<DepositDetails>>> FundingGetDepositHistory(
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
            
            var request = new HttpRequestMessage(HttpMethod.Get, url.ToString());
            SetUpSignatureHeader(request);
            var response = await _httpClient.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Request failed:\nStatus Code:{response.StatusCode}\n{json}");
            }
            
            var baseResponse =  JsonConvert.DeserializeObject<BaseResponse<DepositDetails>>(json);
            return new WebCallResult<IEnumerable<DepositDetails>>(
                response.StatusCode,
                response.Headers,
                baseResponse.Data,
                new WebError(baseResponse.Code, baseResponse.Message, baseResponse.Data));
        }
    }
}