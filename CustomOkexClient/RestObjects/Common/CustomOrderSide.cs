using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CustomCexWrapper.RestObjects.Common
{
    [DataContract]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum CustomOrderSide
    {
        [EnumMember(Value = "buy")]
        Buy,
        [EnumMember(Value = "sell")]
        Sell
    }
}