using System;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CustomCexWrapper.Converters
{
    internal sealed class FormatNumbersAsTextConverter : JsonConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jt = JValue.ReadFrom(reader);

            return jt.Value<decimal>();
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(decimal) == objectType;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value.ToString());
        }
    }
}