using System;
using CustomOkexClient.RestObjects.Responses.PublicData;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CustomOkexClient.Converters
{
    public class OrderDetailsFromStringArrayConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override OrderDetails ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var array = JArray.Load(reader);
            var obj = new OrderDetails();
            for (int i = 0; i < array.Count; i++)
            {
                if (array[i].Type == JTokenType.String)
                {
                    if (i % 4 == 0)
                    {
                        obj.DepthPrice = decimal.Parse(array[i].Value<string>());
                    }
                    else if (i % 4 == 1)
                    {
                        obj.PriceSize = int.Parse(array[i].Value<string>());
                    }
                    else if (i % 4 == 2)
                    {
                        obj.LiquidatedOrdersAmount = int.Parse(array[i].Value<string>());
                    }
                    else if (i % 4 == 3)
                    {
                        obj.OrdersAmount = int.Parse(array[i].Value<string>());
                    }
                }
            }

            return obj;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string[]);
        }
    }
}