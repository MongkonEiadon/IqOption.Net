﻿using IqOptionApi.Models;
using IqOptionApi.Ws.Base;
using Newtonsoft.Json;

namespace IqOptionApi.Ws.Request
{
    internal class SubscribeMessageRequest : WsMessageBase<SubscribeRequestBody>
    {
        //{"name":"subscribeMessage","msg":{"name":"candle-generated","params":{"routingFilters":{"active_id":1,"size":1}}}}
        public SubscribeMessageRequest(ActivePair pair, TimeFrame tf)
        {
            base.Message = new SubscribeRequestBody
            {
                Parameters = new SubscribeRequestParameter
                {
                    Filter = new SubscribeRequestParameter.RequestFilter
                    {
                        ActivePair = pair,
                        TimeFrame = tf
                    }
                }
            };
        }

        public override string Name => MessageType.SubscribeMessage;
    }

    internal class SubscribeRequestBody
    {
        [JsonProperty("name")] public string Name { get; set; } = "candle-generated";

        [JsonProperty("params")] public SubscribeRequestParameter Parameters { get; set; }
    }

    internal class SubscribeRequestParameter
    {
        [JsonProperty("routingFilters")] public RequestFilter Filter { get; set; }


        internal class RequestFilter
        {
            [JsonProperty("active_id")] public ActivePair ActivePair { get; set; }

            [JsonProperty("size")] public TimeFrame TimeFrame { get; set; }
        }
    }
}