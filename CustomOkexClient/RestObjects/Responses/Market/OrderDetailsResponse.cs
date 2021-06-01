using System;
using CustomCexWrapper.Converters;
using CustomCexWrapper.RestObjects.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CustomCexWrapper.RestObjects.Responses.Market
{
    public class OrderDetailsResponse
    {
        /// <summary>
        /// Instrument type: SPOT/MARGIN/SWAP/FUTURES/OPTION
        /// </summary>
        [JsonProperty("instType")]
        public InstrumentType InstrumentType { get; set; }
        
        [JsonProperty("instId")]
        public string Symbol { get; set; }
        
        [JsonProperty("ccy")]
        public string MarginCurrency { get; set; }
        
        /// <summary>
        /// Order ID
        /// </summary>
        [JsonProperty("ordId")]
        public string OrderId { get; set; }
        
        /// <summary>
        /// Client-supplied order ID
        /// </summary>
        [JsonProperty("clOrdId")]
        public string ClientSuppliedOrderId { get; set; }
        
        /// <summary>
        /// Order tag
        /// </summary>
        [JsonProperty("tag")]
        public string Tag { get; set; }
        
        /// <summary>
        /// Quantity to buy or sell.
        /// </summary>
        [JsonProperty("sz")]
        public decimal Quantity { get; set; }

        /// <summary>
        /// Order price. Only applicable to limit order.
        /// </summary>
        [JsonProperty("px")]
        public decimal? OrderPrice { get; set; }

        /// <summary>
        /// Profit and loss
        /// </summary>
        [JsonProperty("pnl")]
        public decimal Profit { get; set; }
        
        /// <summary>
        /// Order Type
        /// </summary>
        [JsonProperty("ordType")]
        public OrderType OrderType { get; set; }
        
        /// <summary>
        /// Order side: buy / sell
        /// </summary>
        [JsonProperty("side")]
        public CustomOrderSide Side { get; set; }
        
        /// <summary>
        /// Position side;
        /// Required in the long/short mode, and can only be long or short.
        /// </summary>
        [JsonProperty("posSide")]
        public OrderPositionSide? PositionSide { get; set; }
        
        /// <summary>
        /// Trade mode:
        /// <p>Margin mode: <code>cross isolated</code></p>
        /// <p>Non-Margin mode: <code>cash</code></p>
        /// </summary>
        [JsonProperty("tdMode")]
        public TradeMode Mode { get; set; }
        
        /// <summary>
        /// Accumulated fill quantity
        /// </summary>
        [JsonProperty("accFillSz")]
        public decimal ExecutedQuantity { get; set; }

        /// <summary>
        /// Last filled price. If none is filled, it will return 0.
        /// </summary>
        [JsonProperty("fillPx")]
        public decimal LastFilledPrice { get; set; }
        
        /// <summary>
        /// Last traded ID
        /// </summary>
        [JsonProperty("tradeId")]
        public string LastTradedId { get; set; }
        
        /// <summary>
        /// Last filled quantity
        /// </summary>
        [JsonProperty("fillSz")]
        public decimal LastFilledQuantity { get; set; }

        /// <summary>
        /// Last filled time
        /// </summary>
        [JsonProperty("fillTime")]
        [JsonConverter(typeof(UnixMillisecondsDateTimeConverter))]
        public DateTime? LastFilledTime { get; set; }
        
        /// <summary>
        /// Average filled price. If none is filled, it will return 0.
        /// </summary>
        [JsonProperty("avgPx")]
        public decimal AverageFilledPrice { get; set; }
        
        /// <summary>
        /// Order state
        /// </summary>
        [JsonProperty("state")]
        public OrderState State { get; set; }
        
        /// <summary>
        /// Leverage, from 0.01 to 125; Only applicable to MARGIN/FUTURES/SWAP
        /// </summary>
        [JsonProperty("lever")]
        public decimal Leverage { get; set; }

        /// <summary>
        /// Take-profit trigger price. It must be between 0 and 1,000,000.
        /// </summary>
        [JsonProperty("tpTriggerPx")]
        public decimal? TakeProfitTriggerPrice { get; set; }
        
        /// <summary>
        /// Take-profit order price. It must be between 0 and 1,000,000.
        /// </summary>
        [JsonProperty("tpOrdPx")]
        public decimal? TakeProfitOrderPrice { get; set; }
        
        /// <summary>
        /// Stop-loss trigger price. It must be between 0 and 1,000,000.
        /// </summary>
        [JsonProperty("slTriggerPx")]
        public decimal? StopLossTriggerPrice { get; set; }
        
        /// <summary>
        /// Stop-loss order price. It must be between 0 and 1,000,000.
        /// </summary>
        [JsonProperty("slOrdPx")]
        public decimal? StopLossOrderPrice { get; set; }
        
        /// <summary>
        /// Fee currency
        /// </summary>
        [JsonProperty("feeCcy")]
        public string FeeCurrency { get; set; }
        
        /// <summary>
        /// Fee
        /// </summary>
        [JsonProperty("fee")]
        public decimal Fee { get; set; }
        
        /// <summary>
        /// Rebate Currency
        /// </summary>
        [JsonProperty("rebateCcy")]
        public string RebateCurrency { get; set; }
        
        /// <summary>
        /// Rebate amount, the reward of placing orders from the platform (rebate) given to user who has reached the specified trading level.
        /// If there is no rebate, this field is "".
        /// </summary>
        [JsonProperty("rebate", DefaultValueHandling = 0)]
        public decimal? Rebate { get; set; }
        
        /// <summary>
        /// Order Category
        /// </summary>
        [JsonProperty("category")]
        public OrderCategory Category { get; set; }
        
        /// <summary>
        /// Update Time
        /// </summary>
        [JsonProperty("uTime")]
        [JsonConverter(typeof(UnixMillisecondsDateTimeConverter))]
        public DateTime UpdateTime { get; set; }
        
        /// <summary>
        /// CreationTime
        /// </summary>
        [JsonProperty("cTime")]
        [JsonConverter(typeof(UnixMillisecondsDateTimeConverter))]
        public DateTime CreationTime { get; set; }
    }
}