using Newtonsoft.Json;

namespace iqoptionapi.http {
    public class IqHttpResult<T> where T : IHttpResultMessage {
        [JsonProperty("isSuccessful")]
        public bool IsSuccessful { get; set; }

        [JsonProperty("message")]
        public object Message { get; set; }

        [JsonProperty("result")]
        public T Result { get; set; } 

        [JsonProperty("location")]
        public string Location { get; set; }

    }


    public class LoginFailedResultMessage : IHttpResultMessage {

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("ttl")]
        public int Ttl { get; set; }

    }
    
}