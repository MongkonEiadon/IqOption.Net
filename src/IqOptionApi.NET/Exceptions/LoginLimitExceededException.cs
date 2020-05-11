﻿using System;

namespace IqOptionApi.exceptions
{
    public class LoginLimitExceededException : Exception
    {
        public LoginLimitExceededException(int ttl) :
            base($"Login limit exceeded, you can re-login in next {ttl} secs.")
        {
        }
    }
}