using Newtonsoft.Json;

namespace iqoptionapi.models {
    public partial class Profile
    {
        [JsonProperty("isSuccessful")]
        public bool IsSuccessful { get; set; }

        [JsonProperty("message")]
        public object[] Message { get; set; }

        [JsonProperty("result")]
        public Profile UserProfile { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }
    }


    public class IqHttpResult<T> where T : class {
        [JsonProperty("isSuccessful")]
        public bool IsSuccessful { get; set; }

        [JsonProperty("message")]
        public object Message { get; set; }

        [JsonProperty("result")]
        public T UserProfile { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

    }
}