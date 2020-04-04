using System.Runtime.CompilerServices;

using Serilog;

namespace IqOptionApi.Extensions {

    internal static class LoggerExtensions {

        public static ILogger WithPayload(this ILogger This, string payloadConent)
            => This.ForContext("Payload", payloadConent);

        public static ILogger WithTopic(this ILogger This, string topicName)
            => This.ForContext("Topic", topicName);


    }

}