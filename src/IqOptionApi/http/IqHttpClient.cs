using System;
using System.Net;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using IqOptionApi.Extensions;
using IqOptionApi.http.commands;
using IqOptionApi.Logging;
using IqOptionApi.Models;
using RestSharp;

namespace IqOptionApi.http {
    public class IqHttpClient : IIqHttpClient {
        public string SecuredToken { get; private set; }
        public LoginModel LoginModel { get; }

        internal IRestClient HttpClient { get; set; }
        internal IRestClient AuthHttpClient { get; set; }

        private readonly ILog _logger = LogProvider.GetLogger("[ HTTP ]");
        private readonly Subject<Profile> _profileSubject = new Subject<Profile>();



        /// <summary>
        /// Stream for profile updated event
        /// </summary>
        /// <returns></returns>
        public IObservable<Profile> ProfileUpdated => _profileSubject.AsQbservable();
        

        public IqHttpClient(string username, string password) {
            LoginModel = new LoginModel {Email = username, Password = password};
            HttpClient = new RestClient( new Uri("https://iqoption.com/api/"));
            AuthHttpClient = new RestClient(new Uri("https://auth.iqoption.com/api/v1.0/"));
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
                        
                    _logger.Debug(L($"Connected:  {result.Data.Ssid}"));

                    HttpClient.CookieContainer = new CookieContainer();
                    HttpClient.CookieContainer?.Add(new Cookie("ssid", SecuredToken, "/", "iqoption.com"));

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
                _logger.Trace(L($"Client ProfileUpdated Updated UserId :{data.UserId}"));

                // published
                _profileSubject.OnNext(data);
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

        private string L(string msg) {
            var name = (LoginModel?.Email ?? "CLIENT");
            return $"[{name, -10}] {msg}";
        }

        #endregion

        public void Dispose() {
            _profileSubject?.Dispose();
        }
    }
}