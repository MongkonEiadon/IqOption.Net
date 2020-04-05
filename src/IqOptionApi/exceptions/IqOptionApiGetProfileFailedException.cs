using System;

namespace IqOptionApi
{
    public class IqOptionApiGetProfileFailedException : Exception
    {
        public IqOptionApiGetProfileFailedException(object receivedContent) : base(
            $"received incorrect content : {receivedContent}")
        {
        }
    }
}