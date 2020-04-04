using System;

namespace IqOptionApi.utilities {

   

    [AttributeUsage(AttributeTargets.Method)]
    public class SubscribeForTopicNameAttribute : Attribute {

        public string TopicName { get; }
        
        public Type ArgumentType { get; }

        public SubscribeForTopicNameAttribute(string topicName, Type argumentType) {
            TopicName = topicName;
            ArgumentType = argumentType;
        }

    }


    [AttributeUsage(AttributeTargets.Method)]
    public class PredisposableAttribute : Attribute {


    }

}