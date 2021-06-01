using Newtonsoft.Json;

namespace CustomCexWrapper.RestObjects.Responses.Market
{
    public class PlaceOrderResponse
    {
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
        /// The code of the event execution result, 0 means success.
        /// </summary>
        [JsonProperty("sCode")]
        public string ExecutionResultCode { get; set; }
        
        /// <summary>
        /// Message shown when the event execution fails.
        /// </summary>
        [JsonProperty("sMsg")]
        public string ExecutionFailedMessage { get; set; }
    }
}