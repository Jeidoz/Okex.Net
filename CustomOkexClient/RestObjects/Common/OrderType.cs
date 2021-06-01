using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CustomCexWrapper.RestObjects.Common
{
    [DataContract]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum OrderType : byte
    {
        [EnumMember(Value = "market")]
        Market,
        [EnumMember(Value = "limit")]
        Limit,
        [EnumMember(Value = "post_only")]
        PostOnly,
        [EnumMember(Value = "fok")]
        FillOrKill,
        [EnumMember(Value = "ioc")]
        ImmediateOrCancel,
        [EnumMember(Value = "optimal_limit_ioc")]
        MarketOrderWithImmediateOrCancel
    }
}