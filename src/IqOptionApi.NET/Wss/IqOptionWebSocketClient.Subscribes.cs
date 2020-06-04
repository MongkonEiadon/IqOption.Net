using System;
using System.Linq;
using System.Reflection;
using IqOptionApi.extensions;
using IqOptionApi.Extensions;
using IqOptionApi.utilities;
using IqOptionApi.Utilities;
using IqOptionApi.Ws.Base;
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
                            foreach (var method in _subscribeMethodInfos.Where(m =>
                                m.Attribute.TopicName == msg.Name))
                            {
                                var args = msg.MessageAs(method.Attribute.ArgumentType);
                                method.TargetMethod.Invoke(this, new[] {args});
                            }
                        else // not support subscribers
                            _logger
                                .LogDebug("Not found handled method to support this kind of message topic '{0}'", msg.Name);


                        _logger.LogTrace("⬇ {0}", x);

                        break;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}