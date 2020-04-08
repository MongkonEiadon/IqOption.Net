using Newtonsoft.Json;

namespace IqOptionApi.Ws.Base
{
    public interface IWsMessage<T> : IWsIqOptionMessageCreator
    {
        [JsonProperty("name")] string Name { get; set; }

        [JsonProperty("msg")] T Message { get; set; }
    }
}