using iqoptionapi.ws.@base;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IqOptionApi.ws.request {

    public interface IWsMessage<T> : IWsIqOptionMessageCreator {

        [JsonProperty("name")] string Name { get; set; }

        [JsonProperty("msg")] T Message { get; set; }

    }

}