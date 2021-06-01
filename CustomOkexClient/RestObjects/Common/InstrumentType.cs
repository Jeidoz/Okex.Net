// ReSharper disable InconsistentNaming

using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CustomCexWrapper.RestObjects.Common
{
    [DataContract]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum InstrumentType : byte
    {
        [EnumMember(Value = "SPOT")]
        Spot,
        [EnumMember(Value = "SWAP")]
        Swap,
        [EnumMember(Value = "FUTURES")]
        Futures,
        [EnumMember(Value = "OPTION")]
        Option
    }
}