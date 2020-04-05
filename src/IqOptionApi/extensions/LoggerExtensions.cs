using Serilog;

namespace IqOptionApi.Extensions
{
    internal static class LoggerExtensions
    {
        public static ILogger WithPayload(this ILogger This, string payloadConent)
        {
            return This.ForContext("Payload", payloadConent);
        }

        public static ILogger WithTopic(this ILogger This, string topicName)
        {
            return This.ForContext("Topic", topicName);
        }
    }
}