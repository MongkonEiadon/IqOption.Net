using System.Linq;
using Newtonsoft.Json;

namespace IqOptionApi.Ws.Base
{
    public class WsMessageWithSuccessfulResult<T> where T : class, new()
    {
        [JsonProperty("isSuccessful")] public bool IsSuccessful { get; set; }

        [JsonProperty("message")] public object[] Message { get; set; }

        [JsonProperty("result")] public T Result { get; set; }

        [JsonProperty("location", Required = Required.Default)]
        public string Location { get; set; }

        public string GetMessageDescription()
        {
            return string.Join(", ", Message ?? Enumerable.Empty<object>());
        }
    }
}