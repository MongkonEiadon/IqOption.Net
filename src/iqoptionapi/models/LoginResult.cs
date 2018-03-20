using Newtonsoft.Json;

namespace iqoptionapi {
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


    public class IqResult<T> where T : class {
        [JsonProperty("isSuccessful")]
        public bool IsSuccessful { get; set; }

        [JsonProperty("message")]
        public object[] Message { get; set; }

        [JsonProperty("result")]
        public T UserProfile { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

    }
}