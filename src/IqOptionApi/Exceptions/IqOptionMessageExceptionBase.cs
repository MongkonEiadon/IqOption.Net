using System;

namespace IqOptionApi.Exceptions
{
    public class IqOptionMessageExceptionBase : Exception
    {
        public object MessageMetaData { get; }

        public IqOptionMessageExceptionBase(object message) : base(message?.ToString())
        {
            MessageMetaData = message;
        }
    }
}