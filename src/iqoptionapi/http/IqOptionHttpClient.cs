﻿using System;
using System.Net;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using IqOptionApi.extensions;
using IqOptionApi.Models;
using RestSharp;
using Serilog;

namespace IqOptionApi.http
{
    public class IqOptionHttpClient
    {

        private readonly ILogger _logger;


        public IqOptionHttpClient(string username, string password, string host = "iqoption.com")
        {
            Client = new RestClient(ApiEndPoint(host));
            LoginModel = new LoginModel { Email = username, Password = password };
            _logger = IqOptionLoggerFactory.CreateLogger();
        }

        public LoginModel LoginModel { get; }


        public SsidResultMessage SecuredToken { get; private set; }
        public IRestClient Client { get; }

        protected static Uri ApiEndPoint(string host)
        {
            return new Uri($"https://{host}/api");
        }

        #region [Profile]

        private readonly Subject<Profile> _profileSubject = new Subject<Profile>();
        private Profile _profile;

        public Profile Profile
        {
            get => _profile;
            private set
            {
                _profileSubject.OnNext(value);
                _profile = value;
            }
        }

        public IObservable<Profile> ProfileObservable() => _profileSubject.Publish().RefCount();

        #endregion


        #region Web-Methods

        public Task<IqHttpResult<SsidResultMessage>> LoginAsync()
        {

            var tcs = new TaskCompletionSource<IqHttpResult<SsidResultMessage>>();
            try
            {
                var client = new RestClient("https://auth.iqoption.com/api/v1.0/login");
                var request = new RestRequest(Method.POST) { RequestFormat = DataFormat.Json }
                    .AddHeader("Content-Type", "application/x-www-form-urlencoded")
                    .AddHeader("content-type", "multipart/form-data")
                    .AddHeader("Accept", "application/json")
                    .AddParameter("email", this.LoginModel.Email, ParameterType.QueryString)
                    .AddParameter("password", this.LoginModel.Password, ParameterType.QueryString);

                client.ExecuteTaskAsync(request)
                    .ContinueWith(t =>
                    {
                        switch (t.Result.StatusCode)
                        {
                            case HttpStatusCode.OK:
                                {
                                    var result = t.Result.Content.JsonAs<IqHttpResult<SsidResultMessage>>();
                                    result.IsSuccessful = true;
                                    SecuredToken = result.Data;

                                    Client.CookieContainer = new CookieContainer();
                                    Client.CookieContainer.Add(new Cookie("ssid", SecuredToken.Ssid, "/", "iqoption.com"));

                                    tcs.TrySetResult(result);
                                    break;
                                }
                            default:
                                {
                                    var error = t.Result.Content.JsonAs<IqHttpResult<SsidResultMessage>>();
                                    error.IsSuccessful = false;
                                    tcs.TrySetResult(error);

                                    break;
                                }
                        }

                    });
            }
            catch (Exception ex)
            {
                tcs.TrySetException(ex);
            }



            return tcs.Task;
        }

        public Task<IqHttpResult<Profile>> GetProfileAsync()
        {
            return ExecuteHttpClientAsync(new GetProfileRequest()).ContinueWith(t =>
            {
                if (t.Result != null && t.Result.StatusCode == HttpStatusCode.OK)
                {
                    return t.Result.Content.JsonAs<IqHttpResult<Profile>>();
                }

                return null;
            });
        }

        public async Task<IqHttpResult<IHttpResultMessage>> ChangeBalanceAsync(long balanceId)
        {
            var result = await ExecuteHttpClientAsync(new ChangeBalanceRequest(balanceId));

            if (result.StatusCode == HttpStatusCode.OK)
                return result.Content.JsonAs<IqHttpResult<IHttpResultMessage>>();

            return null;
        }

        private Task<IRestResponse> ExecuteHttpClientAsync(IRestRequest request)
        {
            var result = Client.ExecuteTaskAsync(request);
            return result;
        }

        #endregion
    }



}