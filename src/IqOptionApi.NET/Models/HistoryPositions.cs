using IqOptionApi.Converters.JsonConverters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace IqOptionApi.Models
{
    public class HistoryPositions
    {
        [JsonProperty("limit")] public int Limit { get; set; }
        [JsonProperty("positions")] public List<Positions> Positions { get; set; }
    }
    public class Positions
    {
        [JsonProperty("active_id")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ActivePair ActivePair { get; set; }
        [JsonProperty("close_profit")]
        public double CloseProfit { get; set; }
        [JsonProperty("close_profit_enrolled")]
        public double CloseProfitEnrolled { get; set; }
        [JsonProperty("close_quote")]
        public double CloseQuote { get; set; }
        [JsonProperty("close_reason")]
        public string CloseReason { get; set; }
        [JsonProperty("close_time")]
        [JsonConverter(typeof(UnixDateTimeJsonConverter))]
        public DateTimeOffset CloseTime { get; set; }
        [JsonProperty("external_id")]
        public long ExternalID { get; set; }
        [JsonProperty("id")]
        public string ID { get; set; }
        [JsonProperty("instrument_id")]
        public string InstrumentId { get; set; }
        [JsonProperty("instrument_type")]
        [JsonConverter(typeof(InstrumentTypeJsonConverter))]
        public InstrumentType InstrumentType { get; set; }
        [JsonProperty("invest")]
        public double InvestAmount { get; set; }
        [JsonProperty("invest_enrolled")]
        public double InvestEnrolled { get; set; }
        [JsonProperty("open_quote")]
        public double OpenQuote { get; set; }
        [JsonProperty("open_time")]
        [JsonConverter(typeof(UnixDateTimeJsonConverter))]
        public DateTimeOffset OpenTime { get; set; }
        [JsonProperty("platform_id")]
        public int PlatformID { get; set; }
        [JsonProperty("pnl")]
        public double Pnl { get; set; }
        [JsonProperty("pnl_net")]
        public double PnlNet { get; set; }
        [JsonProperty("pnl_realized")]
        public double PnlRealized { get; set; }
        [JsonProperty("source")]
        public string Source { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("swap")]
        public double Swap { get; set; }
        [JsonProperty("user_balance_id")]
        public long BalanceID { get; set; }
        [JsonProperty("user_id")]
        public long UserID { get; set; }


        public double getEnrolled()
        {
            double Total = 0;
            if (this.Status == "closed")
            {
                double Enroll = 0;
                if (this.CloseReason == "expired")
                {
                    Enroll = (double)this.CloseProfit - this.InvestAmount;
                }
                if (this.CloseReason == "default")
                {
                    Enroll = this.PnlRealized;
                }
                Total += Enroll;
            }
            return Total;
        }

        public OrderResult getResult()
        {
            return (this.getEnrolled() > 0) ? OrderResult.Win : OrderResult.Loose;
        }
    }
}
