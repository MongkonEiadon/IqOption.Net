﻿using IqOptionApi.Models;
using RestSharp;

namespace IqOptionApi.Http
{
    public class LoginV2Request : IqOptionRequest
    {
        public LoginV2Request(LoginModel loginModel) : base("login", Method.POST)
        {
            AddParameter("email", loginModel.Email);
            AddParameter("password", loginModel.Password);
        }
    }
}