using System;
using System.Linq;
using System.Runtime.CompilerServices;
using IqOptionApi.Converters.JsonConverters;
using IqOptionApi.Models;
using IqOptionApi.Ws.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

[assembly: InternalsVisibleTo("IqOptionApi.Unit", AllInternalsVisible = true)]

namespace IqOptionApi.Ws.Request
{
    internal class BuyV2RequestModel
    {
        [JsonProperty("price", Required = Required.Always)]
        public long Price { get; set; }

        [JsonProperty("active_id", Required = Required.Always)]
        public ActivePair ActivePair { get; set; }


        [JsonProperty("expired", Required = Required.Always)]
        [JsonConverter(typeof(UnixSecondsDateTimeJsonConverter))]
        public DateTimeOffset Expiration { get; set; }


        [JsonProperty("direction", Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        public OrderDirection Direction { get; set; }

        [JsonProperty("user_balance_id")] public long UserBalanceId { get; set; }

        [JsonProperty("option_type_id")] public OptionType OptionType { get; set; }

        [JsonProperty("refund_value")] public int RefundValue { get; set; }

        [JsonProperty("profit_percent")] public long ProfitPercent { get; set; }

        [JsonProperty("value")] public int Value { get; set; }
    }
    
    internal sealed class BuyV2WsMessage : WsSendMessageBase<BuyV2RequestModel>
    {
        public BuyV2WsMessage(
            long balanceId,
            ActivePair pair,
            OptionType optionType,
            OrderDirection direction,
            DateTimeOffset expiration,
            int price)
        {
            Message = new RequestBody<BuyV2RequestModel>
            {
                RequestBodyType = RequestMessageBodyType.OpenOptions,
                Body = new BuyV2RequestModel
                {
                    UserBalanceId = balanceId,
                    ActivePair = pair,
                    OptionType = optionType,
                    Direction = direction,
                    Expiration = expiration,
                    Price = price,

                    // default preset
                    Value = 0,
                    ProfitPercent = 0,
                    RefundValue = 0
                }
            };
        }
    }
}