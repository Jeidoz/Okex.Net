using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CustomCexWrapper.RestObjects.Common
{
    [DataContract]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ContractType : byte
    {
        [EnumMember(Value = "linear")]
        Linear,
        [EnumMember(Value = "inverse")]
        Inverse
    }
}