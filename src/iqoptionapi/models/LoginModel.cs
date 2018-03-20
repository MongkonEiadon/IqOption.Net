using Newtonsoft.Json;

namespace iqoptionapi.models {
    public class LoginModel {
        [JsonProperty("email")]
        public string email { get; set; }

        [JsonProperty("password")]
        public string password { get; set; }
    }
}