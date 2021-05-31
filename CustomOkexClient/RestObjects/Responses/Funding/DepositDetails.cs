using System;
using CustomCexWrapper.RestObjects.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CustomCexWrapper.RestObjects.Responses.Funding
{
    public class DepositDetails
    {
        /// <summary>
        /// Currency
        /// </summary>
        [JsonProperty("ccy")]
        public string Currency { get; set; }

        /// <summary>
        /// Deposit amount
        /// </summary>
        [JsonProperty("amt")]
        public decimal Amount { get; set; }
        
        /// <summary>
        /// Only the internal OKEx account is returned, not the address on the blockchain.
        /// </summary>
        [JsonProperty("from")]
        public DateTime From { get; set; }
        
        /// <summary>
        /// Deposit address
        /// </summary>
        [JsonProperty("to")] 
        public DateTime To { get; set; }

        /// <summary>
        /// Hash record of the deposit
        /// </summary>
        [JsonProperty("txId")]
        public string TxId { get; set; }

        /// <summary>
        /// Time that the deposit is credited, Unix timestamp format in milliseconds, e.g. 1597026383085
        /// </summary>
        [JsonProperty("ts")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime TimeStamp { get; set; }

        /// <summary>
        /// Status of deposit
        /// </summary>
        [JsonProperty("state")]
        public DepositState State { get; set; }

        /// <summary>
        /// Deposit ID
        /// </summary>
        [JsonProperty("depId")]
        public string DepositId { get; set; }
    }
}