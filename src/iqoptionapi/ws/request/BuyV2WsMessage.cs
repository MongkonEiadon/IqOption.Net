using System;
using IqOptionApi.ws.@base;
using IqOptionApi.Converters.JsonConverters;
using IqOptionApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IqOptionApi.ws.request {
    internal class BuyV2RequestModel {
        [JsonProperty("price", Required = Required.Always)]
        public long Price { get; set; }

        [JsonProperty("act", Required = Required.Always)]
        public ActivePair ActivePair { get; set; }


        [JsonProperty("exp", Required = Required.Always)]
        [JsonConverter(typeof(UnixSecondsDateTimeJsonConverter))]
        public DateTimeOffset Expiration { get; set; }


        [JsonProperty("type")]
        public string Type { get; set; } = "turbo";


        [JsonProperty("direction", Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        public OrderDirection Direction { get; set; }


        [JsonProperty("time")]
        [JsonConverter(typeof(UnixSecondsDateTimeJsonConverter))]
        public DateTimeOffset Time { get; set; }
    }


    internal class BuyV2WsMessage : WsMessageBase<BuyV2RequestModel> {
        public BuyV2WsMessage(ActivePair pair, int price, OrderDirection direction, DateTimeOffset expiration, DateTimeOffset now) {
            Message = new BuyV2RequestModel() {
                ActivePair = pair,
                Price = price,
                Direction = direction,
                Expiration = expiration.ToUniversalTime(),
                Time = now.ToUniversalTime()
            };
        }


        public override string Name => "buyV2";
    }
}