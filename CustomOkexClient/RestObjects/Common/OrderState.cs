using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CustomCexWrapper.RestObjects.Common
{
    [DataContract]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum OrderState
    {
        [EnumMember(Value = "canceled")]
        Canceled,
        [EnumMember(Value = "live")]
        Live,
        [EnumMember(Value = "partially_filled")]
        PartiallyFilled,
        [EnumMember(Value = "filled")]
        Filled
    }
}