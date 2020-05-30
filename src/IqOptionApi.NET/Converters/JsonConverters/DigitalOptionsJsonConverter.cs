using System;
using IqOptionApi.Models;
using IqOptionApi.Models.DigitalOptions;
using Newtonsoft.Json;

namespace IqOptionApi.Converters.JsonConverters
{
    public class DigitalOptionsJsonConverter : JsonConverter<DigitalOptionsIdentifier>
    {
        public override void WriteJson(JsonWriter writer, DigitalOptionsIdentifier value, JsonSerializer serializer)
        {
            
            throw new NotImplementedException();
        }

        public override DigitalOptionsIdentifier ReadJson(JsonReader reader, Type objectType, DigitalOptionsIdentifier existingValue, bool hasExistingValue,
            JsonSerializer serializer)
        {
            var value = (string) reader.Value;
            
            
            throw new NotImplementedException();
        }
    }
}