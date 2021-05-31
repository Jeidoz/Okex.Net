using CustomCexWrapper.Converters;
using Newtonsoft.Json;

namespace CustomCexWrapper.RestObjects.Responses.PublicData
{
    [JsonConverter(typeof(OrderDetailsFromStringArrayConverter))]
    public class OrderDetails
    {
        public decimal DepthPrice { get; set; }
        public int PriceSize { get; set; }
        public int LiquidatedOrdersAmount { get; set; }
        public int OrdersAmount { get; set; }
        
        public override string ToString()
        {
            return $"{DepthPrice} - {PriceSize} - {LiquidatedOrdersAmount} - {OrdersAmount}";
        }
    }
}