using System;
using System.Diagnostics;
using System.Net;
using System.Reactive.Linq;
using System.Threading.Tasks;
using iqoptionapi.extensions;
using iqoptionapi.models;
using iqoptionapi.ws;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json.Serialization;
using RestSharp;

namespace iqoptionapi.http {
    public class IqOptionHttpClient : ObservableObject {

        public LoginModel LoginModel { get; }

        private ILogger _logger;
        protected static Uri ApiEndPoint(string host) => new Uri($"https://{host}/api");
        public string SecuredToken { get; private set; }
        public IRestClient Client { get; private set; }

        private Profile _profile;
        public Profile Profile {
            get => _profile;
            private set {
                _profile = value;
                this.OnPropertyChanged(nameof(Profile));
            }
        }

        public IObservable<Profile> ProfileObservable() {
            return this.ToObservable(x => x.Profile);
        }

        public IqOptionHttpClient(string username, string password, string host = "iqoption.com")
        {
            Client = new RestClient(ApiEndPoint(host));
            LoginModel = new LoginModel() {Email = username, Password = password};

            _logger = IqOptionLoggerFactory.CreateLogger();
        }

        #region Web-Methods
        public async Task<IRestResponse> LoginAsync()
        {
            var result = await  Client.ExecuteTaskAsync(new LoginV2Request(LoginModel));

            if (result.StatusCode == HttpStatusCode.OK)
            {
                var loginResult = result.Content.JsonAs<IqHttpResult<Profile>>();
                if (loginResult.IsSuccessful)
                {
                    this.Client.CookieContainer = new CookieContainer();
                    foreach (var c in result.Cookies)
                    {
                        if (c.Name.ToLower() == "ssid")
                        {
                            SecuredToken = c.Value;
                        }
                        this.Client.CookieContainer?.Add(new Cookie(c.Name, c.Value, c.Path, c.Domain));
                    }

                    this.Profile = loginResult.UserProfile;

                }
            }

            return result;
        }

        public async Task<IRestResponse> GetProfileAsync()
        {
            var result = await Client.ExecuteTaskAsync(new GetProfileRequest());
            return result;
        }

        public async Task<IqHttpResult<object>> ChangeBalanceAsync(long balanceId) {
            var result = await Client.ExecuteTaskAsync(new ChangeBalanceRequest(balanceId));

            if (result.StatusCode == HttpStatusCode.OK) {
                return result.Content.JsonAs<IqHttpResult<object>>();
            }
            
            return null;
        }


        #endregion



    }
}

    
