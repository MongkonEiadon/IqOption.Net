using System;
using iqoptionapi.extensions;
using iqoptionapi.http;

namespace iqoptionapi.exceptions {
    public class LoginLimitExceededException : Exception {
        public LoginLimitExceededException(int ttl) :
            base($"Login limit exceeded, you can re-login in next {ttl} secs.") {
        }
    }

    public class LoginFailedException : Exception {
        public LoginFailedException(string emailAddress, LoginFailedMessage message) :
            base($"Login With {emailAddress} not passed!, Response is : {message.AsJson()}") {
            LoginFailedMessage = message;
        }

        public LoginFailedException(string emailAddress, object message) :
            base($"Login With {emailAddress} not passed!, Response is : {message}") {
        }

        public LoginFailedMessage LoginFailedMessage { get; }
    }
}