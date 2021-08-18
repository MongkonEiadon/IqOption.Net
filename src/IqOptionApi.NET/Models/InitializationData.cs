using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IqOptionApi.Models
{
    public class FormatPairInitializationData : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(InitializationData));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);
            List<ActiveInitializationData> result = new List<ActiveInitializationData>();
            IList<string> keys = obj.Properties().Select(p => p.Name).ToList();

            foreach (string key in keys)
            {
                try
                {
                    ActiveInitializationData Data = new ActiveInitializationData();
                    string Pair = (obj[key]["name"].ToString().Replace("front.", "").Trim()).Replace("-","_").Trim();
                    ActivePair ActivePair;
                    if (Enum.TryParse(Pair, out ActivePair) == false) { continue; }
                    Data.ActivePair = ActivePair;
                    Data.Commission = Convert.ToInt32(obj[key]["option"]["profit"]["commission"]);
                    Data.Profit = Math.Abs(100 - Data.Commission);
                    Data.Enabled = Convert.ToBoolean(obj[key]["enabled"]);
                    Data.MaximumBet = Convert.ToInt32(obj[key]["minmax"]["max"]);
                    Data.MinimumBet = Convert.ToInt32(obj[key]["minmax"]["min"]);
                    Data.GroupID = Convert.ToInt32(obj[key]["group_id"]);
                    result.Add(Data);
                }
                catch (Exception)
                {
                    continue;
                }
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

    public class ActiveInitializationData
    {
        public ActivePair ActivePair { get; set; }
        public int Commission { get; set; }
        public int Profit { get; set; }
        public bool Enabled { get; set; }
        public int MinimumBet { get; set; }
        public int MaximumBet { get; set; }
        public int GroupID { get; set; }
    }
    public class PairInitializationData
    {
        [JsonProperty("actives")]
        [JsonConverter(typeof(FormatPairInitializationData))]
        public List<ActiveInitializationData> Actives { get; set; }
    }
    public class InitializationData
    {
        [JsonProperty("binary")]
        public PairInitializationData Binary { get; set; }
        [JsonProperty("turbo")]
        public PairInitializationData Turbo { get; set; }
        public string Currency { get; set; }
    }
}
