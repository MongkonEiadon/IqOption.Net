using iqoptionapi.ws.@base;

// ReSharper disable once CheckNamespace
namespace IqOptionApi.ws.request {
    
    internal class WsSendMessageBase<T> : WsMessageBase<RequestBody<T>> where T : class {

        public override string Name { get; set; } = MessageType.SendMessage;

    }
}