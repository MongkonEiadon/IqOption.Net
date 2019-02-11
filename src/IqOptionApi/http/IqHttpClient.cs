using System;
using System.ComponentModel;
using System.Net;
using System.Reactive.Subjects;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using IqOptionApi.Annotations;
using IqOptionApi.Extensions;
using IqOptionApi.http.Commands;
using IqOptionApi.Logging;
using IqOptionApi.Models;
using RestSharp;

namespace IqOptionApi.http {
    public class IqHttpClient : IIqHttpClient {
        private readonly ILog _logger = LogProvider.GetLogger("[HTTPS]");


        private Profile _profile;
        private string _securedToken;


        public IqHttpClient(string username, string password) {
            LoginModel = new LoginModel {Email = username, Password = password};
            HttpClient = new RestClient(new Uri("https://iqoption.com/api/"));
            AuthHttpClient = new RestClient(new Uri("https://auth.iqoption.com/api/v1.0/"));
        }

        internal IRestClient HttpClient { get; set; }
        internal IRestClient AuthHttpClient { get; set; }


        /// <summary>
        ///     Stream for profile updated event
        /// </summary>
        /// <returns></returns>
        public IObservable<Profile> ProfileUpdated => this.ToObservable(x => x.Profile);

        public string SecuredToken {
            get => _securedToken;
            private set {
                _securedToken = value;
                OnPropertyChanged(nameof(SecuredToken));
            }
        }

        public LoginModel LoginModel { get; }

        public Profile Profile {
            get => _profile;
            set {
                _profile = value;
                OnPropertyChanged(nameof(Profile));
            }
        }

        public void Dispose() {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Web-Methods

        public async Task<IqHttpResult<SsidResultMessage>> LoginAsync() {
            var request = new RestRequest("login", Method.POST) {RequestFormat = DataFormat.Json}
                .AddHeader("Content-Type", "application/x-www-form-urlencoded")
                .AddHeader("content-type", "multipart/form-data")
                .AddHeader("Accept", "application/json")
                .AddParameter("email", LoginModel.Email, ParameterType.QueryString)
                .AddParameter("password", LoginModel.Password, ParameterType.QueryString);

            var httpResult = await AuthHttpClient.ExecuteTaskAsync(request);

            switch (httpResult.StatusCode) {
                case HttpStatusCode.OK: {
                    var result = httpResult.Content.JsonAs<IqHttpResult<SsidResultMessage>>();
                    result.IsSuccessful = true;
                    SecuredToken = result.Data.Ssid;

                    _logger.Debug(L("Connected", result.Data.Ssid));

                    HttpClient.CookieContainer = new CookieContainer();
                    HttpClient.CookieContainer?.Add(new Cookie("ssid", SecuredToken, "/", "iqoption.com"));

                    // update profile
                    await GetProfileAsync();

                    return result;
                }
                default: {
                    var error = httpResult.Content.JsonAs<IqHttpResult<SsidResultMessage>>();
                    error.IsSuccessful = false;

                    return error;
                }
            }
        }


        public async Task<Profile> GetProfileAsync() {
            // send command
            var result = await ExecuteHttpClientAsync(new GetProfileCommand());

            //
            if (result != null && result.StatusCode == HttpStatusCode.OK) {
                var data = result.Content.JsonAs<IqHttpResult<Profile>>().GetContent();

                // log
                _logger.Trace(L( "GetProfile", $"Client Profile Updated UserId :{data.UserId}, trading with BalanceId: {data.BalanceId}"));

                // updated profile
                Profile = data;

                return data;
            }

            return null;
        }


        public async Task<IqHttpResult<IHttpResultMessage>> ChangeBalanceAsync(long balanceId) {
            var result = await ExecuteHttpClientAsync(new ChangeBalanceCommand(balanceId));

            if (result.StatusCode == HttpStatusCode.OK)
                return result.Content.JsonAs<IqHttpResult<IHttpResultMessage>>();

            return null;
        }

        private Task<IRestResponse> ExecuteHttpClientAsync(IRestRequest request) {
            // send command
            var result = HttpClient.ExecuteTaskAsync(request);

            // response
            return result;
        }
        
        private string L(string topic, string msg) {
            var prefix = $"{LoginModel?.Email ?? "CLIENT",13}".Substring(0, 13);

            return $"{prefix.PadRight(13).Substring(0, 13)} | " +
                   $"{topic.PadLeft(13).Substring(0, 13)} > {msg}";
        }

        #endregion
    }
}