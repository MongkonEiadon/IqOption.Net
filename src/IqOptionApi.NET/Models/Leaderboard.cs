using System;
using System.Collections.Generic;
using IqOptionApi.Converters.JsonConverters;
using IqOptionApi.extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace IqOptionApi.Models
{
    public class FormatLeaderboardUser : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(Leaderboard));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);
            List<LeaderboardUsers> result = new List<LeaderboardUsers>();
            IList<string> keys = obj.Properties().Select(p => p.Name).ToList();

            foreach (string key in keys)
            {
                LeaderboardUsers User = new LeaderboardUsers();
                User.Count = Convert.ToInt32(obj[key]["count"]);
                User.Flag = (string)obj[key]["flag"];
                User.Score = (float)Convert.ToDouble(obj[key]["score"]);
                User.UserID = (long)obj[key]["user_id"];
                User.Name = (string)obj[key]["user_name"];
                User.Position = Convert.ToInt32(key);
                result.Add(User);
            }

            return result;
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }

    public class Leaderboard
    {
        [JsonProperty("isSuccessful")]
        public bool isSuccessful { get; set; }
        [JsonProperty("result")]
        public LeaderboardResult Result { get; set; }
    }

    public class LeaderboardResult
    {
        [JsonProperty("country_id")]
        public Country Country { get; set; }
        [JsonProperty("near_traders")]
        [JsonConverter(typeof(FormatLeaderboardUser))]
        public List<LeaderboardUsers> NearTraders { get; set; }
        [JsonProperty("position")]
        public int Position { get; set; }
        [JsonProperty("positional")]
        [JsonConverter(typeof(FormatLeaderboardUser))]
        public List<LeaderboardUsers> Positional { get; set; }
        [JsonProperty("score")]
        public float Score { get; set; }
        [JsonProperty("top")]
        [JsonConverter(typeof(FormatLeaderboardUser))]
        public List<LeaderboardUsers> Top { get; set; }
        [JsonProperty("top_size")]
        public int TopSize { get; set; }
        [JsonProperty("top_type")]
        public int TopType { get; set; }
        [JsonProperty("user_id")]
        public int MyUserID { get; set; }
    }

    public class LeaderboardUsers
    {
        public int Count { get; set; }
        public string Flag { get; set; }
        public float Score { get; set; }
        public long UserID { get; set; }
        public string Name { get; set; }
        public int Position { get; set; }
    }
}
