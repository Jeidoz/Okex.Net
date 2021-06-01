using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CustomCexWrapper.RestObjects.Common
{
    [DataContract]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum FuturesAlias : byte
    {
        [EnumMember(Value = "this_week")]
        ThisWeek,
        [EnumMember(Value = "next_week")]
        NextWeek,
        [EnumMember(Value = "quarter")]
        Quarter,
        [EnumMember(Value = "next_quarter")]
        NextQuarter
    }
}