using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using RestSharp;

namespace IqOptionApi.Tests.Constants
{
    public static class HttpResponseConst
    {
        public static IRestResponse ChangeBalanceSuccess =>
            new RestResponse()
            {
                StatusCode = HttpStatusCode.OK,
                Content = "{ 'isSuccessful': true, 'message': [],  'result': null }"
            };

        public static IRestResponse ChangeIncorrectBalance =>
            new RestResponse()
            {
                StatusCode = HttpStatusCode.OK,
                Content = @"{ 'isSuccessful': false, 'message': 'Balance not your',  'result': null }"
            };

        public static IRestResponse NotLoggedIn =>
            new RestResponse()
            {
                StatusCode = HttpStatusCode.OK,
                Content = @"{ 'isSuccessful': false, 'message': 'Not logged in', 'result': [] }"
            };

        public static IRestResponse NotAuthorized => new RestResponse()
        {
            StatusCode = HttpStatusCode.Unauthorized
        };

        public static IRestResponse InvalidCredentials => new RestResponse()
        {
            StatusCode = HttpStatusCode.Forbidden,
            Content = @"{'errors':[{'code':202,'title':'Invalid credentials'}]}"
        };

        public static IRestResponse LoginSuccess => new RestResponse()
        {
            StatusCode = HttpStatusCode.OK,
            Content = @"{'data':{'ssid':'dcd9f5f493ea3cf89fb48cc449b355ec'}}"
        };
    }
}
