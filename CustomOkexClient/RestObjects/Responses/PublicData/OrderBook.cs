using System;
using System.Collections.Generic;
using System.Text;
using CustomCexWrapper.Converters;
using Newtonsoft.Json;

namespace CustomCexWrapper.RestObjects.Responses.PublicData
{
    public class OrderBook
    {
        /// <summary>
        /// Order book on sell side
        /// </summary>
        [JsonProperty("asks")]
        public List<OrderDetails> Asks { get; set; }
        
        /// <summary>
        /// Order book on buy side
        /// </summary>
        [JsonProperty("bids")]
        public List<OrderDetails> Bids { get; set; }
        
        /// <summary>
        /// Order book generation time
        /// </summary>
        [JsonProperty("ts")]
        [JsonConverter(typeof(MicrosecondEpochConverter))]
        public DateTime GenerationTime { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine("Asks");
            foreach (var ask in Asks)
            {
                builder.AppendLine(ask.ToString()); 
            }
            builder.AppendLine("Bids");
            foreach (var bid in Bids)
            {
                builder.AppendLine(bid.ToString()); 
            }

            return builder.ToString();
        }
    }
}