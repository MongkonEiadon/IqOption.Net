using System;
using IqOptionApi.extensions;
using IqOptionApi.Http;

namespace IqOptionApi.exceptions
{
    public class LoginFailedException : Exception
    {
        public LoginFailedException(string emailAddress, LoginFailedMessage message) :
            base($"Login With {emailAddress} not passed!, Response is : {message.AsJson()}")
        {
            LoginFailedMessage = message;
        }

        public LoginFailedException(string emailAddress, object message) :
            base($"Login With {emailAddress} not passed!, Response is : {message}")
        {
        }

        public LoginFailedMessage LoginFailedMessage { get; }
    }
}