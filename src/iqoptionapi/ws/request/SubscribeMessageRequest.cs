﻿using IqOptionApi.Models;
using IqOptionApi.ws.@base;
using Newtonsoft.Json;

namespace IqOptionApi.ws.Request {
    internal class SubscribeMessageRequest : WsMessageBase<SubscribeRequestBody> {
        public SubscribeMessageRequest(ActivePair pair, TimeFrame tf) {
            base.Message = new SubscribeRequestBody {
                Parameters = new SubscribeRequestParameter {
                    Filter = new SubscribeRequestParameter.RequestFilter {
                        ActivePair = pair,
                        TimeFrame = tf
                    }
                }
            };
        }

        public override string Name => "subscribeMessage";
    }

    internal class SubscribeRequestBody {
        [JsonProperty("name")]
        public string Name { get; set; } = "candle-generated";

        [JsonProperty("params")]
        public SubscribeRequestParameter Parameters { get; set; }
    }

    internal class SubscribeRequestParameter {
        [JsonProperty("routingFilters")]
        public RequestFilter Filter { get; set; }


        internal class RequestFilter {
            [JsonProperty("active_id")]
            public ActivePair ActivePair { get; set; }

            [JsonProperty("size")]
            public TimeFrame TimeFrame { get; set; }
        }
    }
}