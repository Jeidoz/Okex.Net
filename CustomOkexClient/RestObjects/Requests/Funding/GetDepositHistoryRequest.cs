using System;
using CustomCexWrapper.RestObjects.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CustomCexWrapper.RestObjects.Requests.Funding
{
    public class GetDepositHistoryRequest
    {
        /// <summary>
        /// Currency, e.g. BTC
        /// </summary>
        [JsonProperty("ccy")]
        public string Currency { get; set; }

        /// <summary>
        /// Status of deposit
        /// </summary>
        [JsonProperty("state")]
        public DepositState State { get; set; }

        /// <summary>
        /// Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085
        /// </summary>
        [JsonProperty("after")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime After { get; set; }
        
        /// <summary>
        /// Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085
        /// </summary>
        [JsonProperty("before")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime Before { get; set; }

        /// <summary>
        /// Number of results per request. The maximum is 100; the default is 100
        /// </summary>
        [JsonProperty("limit")]
        public int Limit { get; set; } = 100;
    }
}