using Newtonsoft.Json;

namespace iqoptionapi.ws {
    public class WsMsgResult<T> where T : class, new()
    {
        [JsonProperty("isSuccessful")]
        public bool IsSuccessful { get; set; }

        [JsonProperty("message")]
        public object[] Message { get; set; }

        [JsonProperty("result")]
        public T Result { get; set; }

        [JsonProperty("location", Required = Required.Default)]
        public string Location { get; set; }
    }
}