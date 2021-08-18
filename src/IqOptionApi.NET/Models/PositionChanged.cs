using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using IqOptionApi.Converters.JsonConverters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace IqOptionApi.Models
{

    public class FormatRawData : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(PositionChanged));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);
            List<PortfolioChangedEventInfo> result = new List<PortfolioChangedEventInfo>();
            IList<string> keys = obj.Properties().Select(p => p.Name).ToList();

            foreach (string key in keys)
            {
                JToken Data = obj[key];
                string ConvertToJSON = JsonConvert.SerializeObject(Data);
                PortfolioChangedEventInfo Raw = JsonConvert.DeserializeObject<PortfolioChangedEventInfo>(ConvertToJSON);
                result.Add(Raw);
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

    public class PositionChanged
    {
        [JsonProperty("id")] public string Id { get; set; }
        
        [JsonProperty("instrument_id")] public string InstrumentId { get; set; }
        
        [JsonProperty("instrument_type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public InstrumentType InstrumentType { get; set; }
        
        [JsonProperty("active_id")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ActivePair ActivePair { get; set; }
        
        [JsonProperty("invest")]
        public double InvestAmount { get; set; }
        
        [JsonProperty("open_quote")]
        public double OpenQuote { get; set; }

        [JsonProperty("close_quote")]
        public double? CloseQuote { get; set; }

        [JsonProperty("close_time")]
        [JsonConverter(typeof(UnixDateTimeJsonConverter))]
        public DateTimeOffset CloseTime { get; set; }

        [JsonProperty("open_time")]
        [JsonConverter(typeof(UnixDateTimeJsonConverter))]
        public DateTimeOffset OpenTime { get; set; }

        [JsonProperty("raw_event")]
        [JsonConverter(typeof(FormatRawData))]
        public List<PortfolioChangedEventInfo> PortfolioChangedEventInfo { get; set; }

        [JsonProperty("close_profit")]
        public double? CloseProfit { get; set; }


        public new bool Equals(object x, object y)
        {
            return x == y;
        }

        public int GetHashCode(object obj)
        {
            return obj.GetHashCode();
        }

        public double getEnrolled()
        {
            double Total = 0;
            foreach (PortfolioChangedEventInfo portfolio in this.PortfolioChangedEventInfo) {
                if (portfolio.Status == "closed")
                {
                    double Enroll = 0;
                    if (portfolio.CloseReason == "expired")
                    {
                        Enroll = (double)this.CloseProfit - this.InvestAmount;
                    }
                    if (portfolio.CloseReason == "default")
                    {
                        Enroll = portfolio.PnlRealized;
                    }
                    Total += Enroll;
                }
            }
            return Total;
        }

        public OrderResult getResult()
        {
            return (this.getEnrolled() > 0) ? OrderResult.Win : OrderResult.Loose;
        }
    }

    public class PortfolioChangedEventInfo
    {
        [JsonProperty("instrument_dir")]
        [JsonConverter(typeof(StringEnumConverter))]
        public OrderDirection OrderDirection { get; set; }

        [JsonProperty("instrument_active_id")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ActivePair ActivePair { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
        
        [JsonProperty("type")]
        public string Type { get; set; }
        
        [JsonProperty("user_balance_id")]
        public long UserBalanceId { get; set; }

        [JsonProperty("user_balance_type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public BalanceType UserBalanceType { get; set; }

        [JsonProperty("instrument_expiration")]
        [JsonConverter(typeof(UnixDateTimeJsonConverter))]
        public DateTimeOffset? ExpirationTime { get; set; }

        [JsonProperty("instrument_period")]
        public int? PeriodTime { get; set; }

        [JsonProperty("user_id")]
        public long UserId { get; set; }

        [JsonProperty("order_ids")]
        public List<long> OrderID { get; set; }

        [JsonProperty("pnl_realized")]
        public double PnlRealized { get; set; }
        [JsonProperty("close_reason")]
        public string CloseReason { get; set; }
        [JsonProperty("buy_amount")]
        public double BetAmount { get; set; }
    }
}