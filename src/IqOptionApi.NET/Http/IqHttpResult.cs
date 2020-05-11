﻿using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace IqOptionApi.Http
{
    public class IqHttpResult<T> where T : IHttpResultMessage
    {
        [JsonProperty("isSuccessful")] public bool IsSuccessful { get; set; }

        [JsonProperty("message")] public object Message { get; set; }

        [JsonProperty("result")] public T Result { get; set; }

        [JsonProperty("data")] public T Data { get; set; }

        [JsonProperty("location")] public string Location { get; set; }

        [JsonProperty("errors")] public Errors Errors { get; set; }
    }

    public class Errors : List<ErrorResult>
    {
        public string GetErrorMessage()
        {
            return string.Join(", ", this.Select(x => x.Message));
        }
    }

    public class ErrorResult
    {
        [JsonProperty("code")] public int Code { get; set; }

        [JsonProperty("title")] public string Message { get; set; }
    }

    public class LoginFailedResultMessage
    {
        [JsonProperty("isSuccessful")] public bool IsSuccessful { get; set; }

        [JsonProperty("message")] public LoginFailedMessage Message { get; set; }

        [JsonProperty("result")] public string[] Result { get; set; }
    }


    public class LoginFailedMessage
    {
        [JsonProperty("email")] public string[] Email { get; set; }

        [JsonProperty("password")] public string[] Password { get; set; }
    }


    public class LoginTooMuchResultMessage : IHttpResultMessage
    {
        [JsonProperty("status")] public string Status { get; set; }

        [JsonProperty("ttl")] public int Ttl { get; set; }
    }

    public class SsidResultMessage : IHttpResultMessage
    {
        [JsonProperty("ssid")] public string Ssid { get; set; }
    }
}