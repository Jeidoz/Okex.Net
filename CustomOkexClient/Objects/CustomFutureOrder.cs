using System;

namespace CustomCexWrapper.Objects
{
    public class CustomFutureOrder
    {
        public decimal ExecutedQuantity { get; set; }
        public decimal AvgPrice { get; set; }
        public DateTime CreatedTime { get; set; }
        public string Status { get; set; }
        public decimal OriginalQuantity { get; set; }
        public decimal Price { get; set; }
        public long OrderId { get; set; }
        public string Symbol { get; set; }
    }
}