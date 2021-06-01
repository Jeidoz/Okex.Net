using System;
using CustomCexWrapper.Converters;
using CustomCexWrapper.RestObjects.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CustomCexWrapper.RestObjects.Responses.PublicData
{
    public class TradeInstrument
    {
        /// <summary>
        ///     Instrument type
        /// </summary>
        [JsonProperty("instType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public InstrumentType Type { get; set; }

        /// <summary>
        ///     Instrument ID
        /// </summary>
        [JsonProperty("instId")]
        public string Id { get; set; }

        /// <summary>
        ///     Underlying, e.g. BTC-USD;
        ///     Only applicable to FUTURES/SWAP/OPTION
        /// </summary>
        [JsonProperty("uly")]
        public string Underlying { get; set; }

        /// <summary>
        ///     Fee schedule
        /// </summary>
        [JsonProperty("category")]
        public string Category { get; set; }

        /// <summary>
        ///     Base currency, e.g. BTC inBTC-USDT;
        ///     Only applicable to SPOT
        /// </summary>
        [JsonProperty("baseCcy")]
        public string BaseCurrency { get; set; }

        /// <summary>
        ///     Quote currency, e.g. USDT in BTC-USDT;
        ///     Only applicable to SPOT
        /// </summary>
        [JsonProperty("quoteCcy")]
        public string QuoteCurrency { get; set; }

        /// <summary>
        ///     Settlement and margin currency, e.g. BTC;
        ///     Only applicable to FUTURES/SWAP/OPTION
        /// </summary>
        [JsonProperty("settleCcy")]
        public string SettlementAndMarginCurrency { get; set; }

        /// <summary>
        ///     Contract value;
        ///     Only applicable to FUTURES/SWAP/OPTION
        /// </summary>
        [JsonProperty("ctVal")]
        public decimal? ContractValue { get; set; }

        /// <summary>
        ///     Contract multiplier;
        ///     Only applicable to FUTURES/SWAP/OPTION
        /// </summary>
        [JsonProperty("ctMult")]
        public decimal? ContractMultiplier { get; set; }

        /// <summary>
        ///     Contract value currency;
        ///     Only applicable to FUTURES/SWAP/OPTION
        /// </summary>
        [JsonProperty("ctValCcy")]
        public string ContractMul { get; set; }

        /// <summary>
        ///     Option type, C: Call P: put;
        ///     Only applicable to OPTION
        /// </summary>
        [JsonProperty("optType")]
        public string OptionType { get; set; }

        /// <summary>
        ///     Strike price;
        ///     Only applicable to OPTION
        /// </summary>
        [JsonProperty("stk")]
        public string StrikePrice { get; set; }

        /// <summary>
        ///     Listing time, Unix timestamp format in milliseconds, e.g. 1597026383085
        /// </summary>
        [JsonProperty("listTime")]
        [JsonConverter(typeof(UnixMillisecondsDateTimeConverter))]
        public DateTime? ListingTime { get; set; }

        /// <summary>
        ///     Expiry time, Unix timestamp format in milliseconds, e.g. 1597026383085;
        ///     Only applicable to FUTURES/OPTION
        /// </summary>
        [JsonProperty("expTime")]
        [JsonConverter(typeof(UnixMillisecondsDateTimeConverter))]
        public DateTime? ExpiryTime { get; set; }

        /// <summary>
        ///     Leverage;
        ///     Not applicable to SPOT, used to distinguish between MARGIN and SPOT.
        /// </summary>
        [JsonProperty("lever")]
        public string Leverage { get; set; }

        /// <summary>
        ///     Tick size, e.g. 0.0001
        /// </summary>
        [JsonProperty("tickSz")]
        public decimal TickSize { get; set; }

        /// <summary>
        ///     Lot size, e.g. BTC-USDT-SWAP: <code>1</code>
        /// </summary>
        [JsonProperty("lotSz")]
        public decimal LotSize { get; set; }

        /// <summary>
        ///     Minimum order size
        /// </summary>
        [JsonProperty("minSz")]
        public decimal MinimumOrderSize { get; set; }

        /// <summary>
        ///     Contract type;
        ///     Only applicable to FUTURES/SWAP
        /// </summary>
        [JsonProperty("ctType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ContractType? ContractType { get; set; }

        /// <summary>
        ///     Alias;
        ///     Only applicable to FUTURES
        /// </summary>
        [JsonProperty("alias")]
        [JsonConverter(typeof(StringEnumConverter))]
        public FuturesAlias? Alias { get; set; }

        /// <summary>
        ///     Instrument status
        /// </summary>
        [JsonProperty("state")]
        [JsonConverter(typeof(StringEnumConverter))]
        public InstrumentStatus Status { get; set; }
    }
}