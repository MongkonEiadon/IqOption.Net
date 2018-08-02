using System;
using System.Net;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using iqoptionapi.exceptions;
using iqoptionapi.extensions;
using iqoptionapi.models;
using iqoptionapi.ws;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;

namespace iqoptionapi.http {
    public class IqOptionHttpClient : ObservableObject {
        private readonly ILogger _logger;

        public IqOptionHttpClient(string username, string password, string host = "iqoption.com") {
            Client = new RestClient(ApiEndPoint(host));
            LoginModel = new LoginModel {Email = username, Password = password};
            _logger = IqOptionLoggerFactory.CreateLogger();
        }

        public LoginModel LoginModel { get; }


        public string SecuredToken { get; private set; }
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

        public Task<LoginResult> LoginAsync() {

            var tcs = new TaskCompletionSource<LoginResult>();
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
                    .ContinueWith(t => {
                        switch (t.Result.StatusCode)
                        {
                            case HttpStatusCode.OK:
                                {
                                    var result = t.Result.Content.JsonAs<LoginResult>();
                                    tcs.TrySetResult(result);
                                    break;
                                }

                            case HttpStatusCode.BadRequest:
                                {
                                    //var error = t.Result.Content.JsonAs<LoginErrorCommandResult>();
                                    //tcs.TrySetResult(new IqLoginCommandResult(null, false, string.Join(",",
                                    //    error.Errors?.Select(x => x.Title)?.ToList())));

                                    break;
                                }

                            case HttpStatusCode.Forbidden:
                                {
                                    //var error = t.Result.Content.JsonAs<LoginErrorCommandResult>();
                                    //tcs.TrySetResult(new IqLoginCommandResult(null, false, string.Join(",",
                                    //    error.Errors?.Select(x => x.Title)?.ToList())));

                                    break;
                                }
                        }

                        tcs.TrySetException(new Exception($"Error when get token with {t.Result.Content}"));
                    });
            }
            catch (Exception ex)
            {
                tcs.TrySetException(ex);
            }



            return tcs.Task;
        }

        public Task<IRestResponse> GetProfileAsync() {
            return ExecuteHttpClientAsync(new GetProfileRequest());
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


    public class LoginResult : HttpCommandResult<SsidResult>
    {
    }

    public class IqHttpResult<T> where T : class
    {
        [JsonProperty("isSuccessful")]
        public bool IsSuccessful { get; set; }

        [JsonProperty("message")]
        public object Message { get; set; }

        [JsonProperty("result")]
        public T Result { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }
    }

    internal class HttpCommandResult<T>
    {
        [JsonProperty("data")]
        public T Result { get; set; }
    }

    internal class SsidResult
    {
        [JsonProperty("ssid")]
        public string Ssid { get; set; }
    }
}