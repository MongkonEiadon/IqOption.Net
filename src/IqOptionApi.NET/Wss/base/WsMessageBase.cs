﻿using System;
using IqOptionApi.extensions;
using Newtonsoft.Json;

namespace IqOptionApi.Ws.Base
{
    public class WsMessageBase<T> : IWsMessage<T>, IWsIqOptionMessageCreator
    {
        [JsonProperty("request_id", Order = 2)] public virtual string RequestId { get; set; }
        [JsonProperty("local_time", Order = 3)] public virtual int LocalTime { get; set; }
        [JsonProperty("name", Order = 1)] public virtual string Name { get; set; }
        
        [JsonProperty("version", Order = 11, NullValueHandling = NullValueHandling.Ignore)] public virtual string Version { get; set; } = "1.0";
        [JsonProperty("msg", Order = 4)] public virtual T Message { get; set; }
        
        [JsonProperty("status", Order = Int32.MaxValue, NullValueHandling = NullValueHandling.Ignore)] public virtual int? StatusCode { get; set; }
        
        [JsonProperty("microserviceName", Order = 10, NullValueHandling = NullValueHandling.Ignore)] public virtual string MicroserviceName { get; set; }
        
        public virtual string CreateIqOptionMessage(string requestId)
        {
            RequestId = requestId;
            LocalTime = int.Parse(requestId.Replace("s_", "")) * 1000;
            return this.AsJson();
        }

        public virtual string GetRequestID()
        {
            return RequestId;
        }
        public override string ToString()
        {
            return this.AsJson();
        }

        public TType MessageAs<TType>()
        {
            return Message.ToString().JsonAs<TType>();
        }

        public object MessageAs(Type type)
        {
            return Message.ToString().JsonAs(type);
        }
    }
}