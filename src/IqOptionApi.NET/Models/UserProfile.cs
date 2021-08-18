using System;
using System.Collections.Generic;
using System.Text;
using IqOptionApi.Converters.JsonConverters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IqOptionApi.Models
{
    public class UserProfile
    {
        [JsonProperty("country_id")]
        public int CountryID { get; set; }
        [JsonProperty("flag")]
        public string Flag { get; set; }
        [JsonProperty("gender")]
        public int Gender { get; set; }
        [JsonProperty("img_url")]
        public string ImageURL { get; set; }
        [JsonProperty("isSuccessful")]
        public bool IsSuccessful { get; set; }
        [JsonProperty("is_demo_account")]
        public bool IsDemoAccount { get; set; }
        [JsonProperty("is_vip")]
        public bool IsVIP { get; set; }
        [JsonProperty("registration_time")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTimeOffset RegistrationTime { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("user_id")]
        public long UserID { get; set; }
        [JsonProperty("user_name")]
        public string Name { get; set; }
        [JsonProperty("vip_badge")]
        public bool VIPBadge { get; set; }
    }
}
