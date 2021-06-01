using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CustomCexWrapper.RestObjects.Common
{
    [DataContract]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum OrderCategory
    {
        [EnumMember(Value = "normal")]
        Normal,
        [EnumMember(Value = "twap")]
        TimeWeightedAveragePrice,
        [EnumMember(Value = "adl")]
        AutoDeleveraging,
        [EnumMember(Value = "full_liquidation")]
        FullLiquidation,
        [EnumMember(Value = "partial_liquidation")]
        PartialLiquidation,
        [EnumMember(Value = "delivery")]
        Delivery
    }
}