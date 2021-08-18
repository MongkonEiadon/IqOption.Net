using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using IqOptionApi.extensions;
using IqOptionApi.utilities;
using IqOptionApi.Utilities;
using IqOptionApi.Ws.Base;
using IqOptionApi.Wss;
using Microsoft.Extensions.Logging;

namespace IqOptionApi.Ws
{
    public partial class IqOptionWebSocketClient
    {
        private static readonly MethodInvoker<SubscribeForTopicNameAttribute>[] _subscribeMethodInfos =
            typeof(IqOptionWebSocketClient).GetMethods()
                .Where(x => x.GetCustomAttribute(typeof(SubscribeForTopicNameAttribute)) != null)
                .Select(x =>
                    new MethodInvoker<SubscribeForTopicNameAttribute>(
                        (SubscribeForTopicNameAttribute) x.GetCustomAttribute(typeof(SubscribeForTopicNameAttribute)),
                        x)
                ).ToArray();


        internal void SubscribeIncomingMessage(string x)
        {
            try
            {
                var msg = x.JsonAs<WsMessageBase<object>>();

                SystemReconnectionTimer.BeginInit();

                switch (msg.Name)
                {
                    case MessageType.Heartbeat:
                        SetHeartbeatTick((long) msg.Message);
                        break;

                    case MessageType.TimeSync:
                        SetServerTime((long) msg.Message);
                        break;

                    default:

                        // list of subs.
                        var methods = _subscribeMethodInfos.Where(m => m.Attribute.TopicName == msg.Name);

                        // invoke subscribers
                        if (methods.Any())
                            Parallel.ForEach(_subscribeMethodInfos.Where(m => m.Attribute.TopicName == msg.Name), method =>
                            {
                                var AttributeInfo = method.Attribute.ArgumentType;
                                var invokeArgs = ((method.Attribute.Callback) ? new[] { x.JsonAs(AttributeInfo) } : new[] { msg.MessageAs(method.Attribute.ArgumentType) });
                                method.TargetMethod.Invoke(this, invokeArgs);
                            });
                        else // not support subscribers
                            _logger.LogDebug("Not found handled method to support this kind of message topic '{0}'", msg.Name);

                        bool AnyInvoker = this.InvokeIncomingMessages.Keys.Any(key => key == msg.RequestId);
                        if (AnyInvoker)
                        {
                            if (this.InvokeIncomingMessages[msg.RequestId].Count > 0) {
                                foreach(IncomingMessageInvoker invoker in this.InvokeIncomingMessages[msg.RequestId])
                                {
                                    object Callback = msg.MessageAs(invoker.Type);
                                    invoker.Action(Callback);
                                };
                            }
                            lock (this.InvokeIncomingMessages)
                            {
                                this.InvokeIncomingMessages[msg.RequestId] = new List<IncomingMessageInvoker>();
                            }
                        }

                        _logger.LogTrace("⬇ {0}", x);

                        break;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}");
            }
        }

        private Dictionary<string, List<IncomingMessageInvoker>> InvokeIncomingMessages = new Dictionary<string, List<IncomingMessageInvoker>>();
        public void InvokeIncomingMessage<T>(string RequestID, Action<T> Action)
        {
            lock (this.InvokeIncomingMessages) {
                Type type = typeof(T);
                if (!this.InvokeIncomingMessages.ContainsKey(RequestID)) this.InvokeIncomingMessages.Add(RequestID, new List<IncomingMessageInvoker>());
                this.InvokeIncomingMessages[RequestID].Add(new IncomingMessageInvoker()
                {
                    Type = type,
                    Action = new Action<object>(o => Action((T)o))
                });
            }
        }
    }
}