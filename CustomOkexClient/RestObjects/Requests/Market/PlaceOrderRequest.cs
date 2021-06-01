using CustomCexWrapper.Converters;
using CustomCexWrapper.RestObjects.Common;
using Newtonsoft.Json;

namespace CustomCexWrapper.RestObjects.Requests.Market
{
    public class PlaceOrderRequest
    {
        [JsonProperty("instId")]
        public string Symbol { get; set; }

        /// <summary>
        /// Trade mode:
        /// <p>Margin mode: <code>cross isolated</code></p>
        /// <p>Non-Margin mode: <code>cash</code></p>
        /// </summary>
        [JsonProperty("tdMode")]
        public TradeMode Mode { get; set; }

        /// <summary>
        /// Margin currency;
        /// Only applicable to cross MARGIN orders in Single-currency margin.
        /// </summary>
        [JsonProperty("ccy")]
        public string MarginCurrency { get; set; }
        
        /// <summary>
        /// Client-supplied order ID; 
        /// A combination of case-sensitive alphanumerics, all numbers, or all letters of up to 32 characters.
        /// </summary>
        [JsonProperty("clOrdId")]
        public string ClientSuppliedOrderId { get; set; }

        /// <summary>
        /// Order tag; 
        /// A combination of case-sensitive alphanumerics, all numbers, or all letters of up to 8 characters.
        /// </summary>
        [JsonProperty("tag")]
        public string Tag { get; set; }

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
        /// Order Type
        /// </summary>
        [JsonProperty("ordType")]
        public OrderType OrderType { get; set; }
        
        /// <summary>
        /// Quantity to buy or sell.
        /// </summary>
        [JsonProperty("sz")]
        [JsonConverter(typeof(FormatNumbersAsTextConverter))]
        public decimal Quantity { get; set; }

        /// <summary>
        /// Order price. Only applicable to limit order.
        /// </summary>
        [JsonProperty("px")]
        [JsonConverter(typeof(FormatNumbersAsTextConverter))]
        public decimal LimitPrice { get; set; }

        /// <summary>
        /// Whether to reduce position only or not, true false, the default is false.
        /// Only applicable for MARGIN orders
        /// </summary>
        [JsonProperty("reduceOnly")]
        public bool ReduceOnly { get; set; }

        public bool ShouldSerializeMarginCurrency()
        {
            return Mode != TradeMode.Cash;
        }
        
        public bool ShouldSerializeReduceOnly()
        {
            return Mode != TradeMode.Cash;
        }
        
        public bool ShouldSerializeLimitPrice()
        {
            return OrderType == OrderType.Limit;
        }
    }
}