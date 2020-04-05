using Newtonsoft.Json;

namespace IqOptionApi.ws.request
{
    public interface IWsResultMessage<T> : IWsIqOptionMessageCreator
    {

        [JsonProperty("name")]
        string Name { get; set; }
        [JsonProperty("request_id")]
        string RequestId { get; set; }
        [JsonProperty("microserviceName")]
        string MicroserviceName { get; set; }

        [JsonProperty("msg")]
        T Message { get; set; }
    }
}