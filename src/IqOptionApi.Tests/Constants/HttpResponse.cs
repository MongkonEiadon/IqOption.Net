using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using RestSharp;

namespace IqOptionApi.Tests.Constants
{
    public static class HttpConstants
    {
        public static IRestResponse NoLoggedIn =>
            new RestResponse()
            {
                StatusCode = HttpStatusCode.OK,
                Content = @"{'isSuccessful':false,'message':'Not logged in','result':[]}"
            };
    }
}
