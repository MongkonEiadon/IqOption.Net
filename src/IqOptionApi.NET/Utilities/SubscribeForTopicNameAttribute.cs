using System;

namespace IqOptionApi.utilities
{
    [AttributeUsage(AttributeTargets.Method)]
    public class SubscribeForTopicNameAttribute : Attribute
    {
        public SubscribeForTopicNameAttribute(string topicName, Type argumentType, bool Callback=false)
        {
            this.TopicName = topicName;
            this.ArgumentType = argumentType;
            this.Callback = Callback;
        }

        public string TopicName { get; }

        public Type ArgumentType { get; }

        public bool Callback { get; }
    }


    [AttributeUsage(AttributeTargets.Method)]
    public class PredisposableAttribute : Attribute
    {
    }
}