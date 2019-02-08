using System;
using System.Net;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using IqOptionApi.exceptions;
using IqOptionApi.Extensions;
using IqOptionApi.ws;
using IqOptionApi.Models;
using Newtonsoft.Json;
using RestSharp;
using Serilog;

namespace IqOptionApi.http {
    public interface IIqHttpClient {

        LoginModel LoginModel { get; }

        Task<IqHttpResult<SsidResultMessage>> LoginAsync();

        
    }

    public class IqHttpClient : IIqHttpClient {
        public LoginModel LoginModel { get; private set; }

        internal IRestClient HttpClient { get; set; }
        internal IRestClient AuthHttpClient { get; set; }



        public IqHttpClient(string username, string password) {

            LoginModel = new LoginModel() {Email = username, Password = password};


            HttpClient = new RestClient("https://iqoption.com/api");
            AuthHttpClient = new RestClient("https://auth.iqoption.com/api/v1.0/login");
        }


        //public IqHttpClient(string username, string password, string host = "iqoption.com") {

        //    // set the httpClient
        //    //HttpHttpClient = new RestClient(ApiEndPoint(host));


        //    //Client = new RestClient(ApiEndPoint(host));
        //    //LoginModel = new LoginModel {Email = username, Password = password};
        //    //_logger = IqOptionLoggerFactory.CreateLogger();
        //}
        


        public SsidResultMessage SecuredToken { get; private set; }
        public IRestClient Client { get; }

        protected static Uri ApiEndPoint(string host) {
            return new Uri($"https://{host}/api");
        }

#region [Profile]

        private readonly Subject<Profile> _profileSubject = new Subject<Profile>();
        private Profile _profile;

        public Profile Profile {
            get => _profile;
            private set {
                _profileSubject.OnNext(value);
                _profile = value;
            }
        }

        public IObservable<Profile> ProfileObservable() => _profileSubject.Publish().RefCount();

#endregion


#region Web-Methods

        public Task<IqHttpResult<SsidResultMessage>> LoginAsync() {

            var tcs = new TaskCompletionSource<IqHttpResult<SsidResultMessage>>();
            try
            {
                //var client = new RestClient("https://auth.iqoption.com/api/v1.0/login");
                
                var request = new RestRequest(Method.POST) { RequestFormat = DataFormat.Json }
                    .AddHeader("Content-Type", "application/x-www-form-urlencoded")
                    .AddHeader("content-type", "multipart/form-data")
                    .AddHeader("Accept", "application/json")
                    .AddParameter("email", this.LoginModel.Email, ParameterType.QueryString)
                    .AddParameter("password", this.LoginModel.Password, ParameterType.QueryString);

                AuthHttpClient.ExecuteTaskAsync(request)
                    .ContinueWith(t => {
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
                            default :
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
            return ExecuteHttpClientAsync(new GetProfileRequest()).ContinueWith(t => {
                if (t.Result != null && t.Result.StatusCode == HttpStatusCode.OK) {
                    return t.Result.Content.JsonAs<IqHttpResult<Profile>>();
                }

                return null;
            });
        }

        public async Task<IqHttpResult<IHttpResultMessage>> ChangeBalanceAsync(long balanceId) {
            var result = await ExecuteHttpClientAsync(new ChangeBalanceRequest(balanceId));

            if (result.StatusCode == HttpStatusCode.OK)
                return result.Content.JsonAs<IqHttpResult<IHttpResultMessage>>();

            return null;
        }

        private Task<IRestResponse> ExecuteHttpClientAsync(IRestRequest request) {
            var result = Client.ExecuteTaskAsync(request);
            return result;
        }

#endregion
    }


  
}