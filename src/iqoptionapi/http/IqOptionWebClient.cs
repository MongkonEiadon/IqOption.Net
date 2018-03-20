using System;
using System.Diagnostics;
using System.Net;
using System.Reactive.Linq;
using System.Threading.Tasks;
using iqoption.core.Converters.JsonConverters;
using iqoption.core.Extensions;
using iqoptionapi.models;
using iqoptionapi.ws;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json.Serialization;
using RestSharp;

namespace iqoptionapi.http {
    public class IqOptionWebClient {

        public LoginModel LoginModel { get; }
        protected static Uri ApiEndPoint(string host) => new Uri($"https://{host}/api");
        public string SecuredToken { get; private set; }
        public IRestClient Client { get; private set; }
        public Profile Profile { get; private set; }

        public IqOptionWebClient(string username, string password, string host = "iqoption.com")
        {
            Client = new RestClient(ApiEndPoint(host));
            LoginModel = new LoginModel() {email = username, password = password};
        }

        #region Web-Methods
        public async Task<IRestResponse> LoginAsync()
        {
            var result = await  Client.ExecuteTaskAsync(new LoginV2Request(LoginModel));

            if (result.StatusCode == HttpStatusCode.OK)
            {
                var loginResult = result.Content.JsonAs<IqResult<Profile>>();
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

        public async Task<IRestResponse> ChangeBalanceAsync(long balanceId) {
            var result = await Client.ExecuteTaskAsync(new ChangeBalanceRequest(balanceId));
            return result;
        }


        #endregion



    }
}

    
