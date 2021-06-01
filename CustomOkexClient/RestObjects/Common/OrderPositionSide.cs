using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CustomCexWrapper.RestObjects.Common
{
    [DataContract]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum OrderPositionSide
    {
        [EnumMember(Value = null)]
        None,
        [EnumMember(Value = "long")]
        Long,
        [EnumMember(Value = "short")]
        Short,
        [EnumMember(Value = "net")]
        Net
    }
}