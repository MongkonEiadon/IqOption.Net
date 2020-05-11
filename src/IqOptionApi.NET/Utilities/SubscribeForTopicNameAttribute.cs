using System;

namespace IqOptionApi.utilities
{
    [AttributeUsage(AttributeTargets.Method)]
    public class SubscribeForTopicNameAttribute : Attribute
    {
        public SubscribeForTopicNameAttribute(string topicName, Type argumentType)
        {
            TopicName = topicName;
            ArgumentType = argumentType;
        }

        public string TopicName { get; }

        public Type ArgumentType { get; }
    }


    [AttributeUsage(AttributeTargets.Method)]
    public class PredisposableAttribute : Attribute
    {
    }
}