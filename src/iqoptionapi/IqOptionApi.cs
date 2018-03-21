using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using iqoptionapi.extensions;
using iqoptionapi.http;
using iqoptionapi.ws;
using Microsoft.Extensions.Options;

namespace iqoptionapi {

    public class IqOptionApi {
        private string _host { get; }

        public IqOptionWebClient WebClient { get; }
        public IqOptionWebSocketClient WsClient { get; private set; }


        public IqOptionApi(string username, string password, string host = "iqoption.com") {
            _host = host;

            //set up client
            WebClient = new IqOptionWebClient(username, password);
        }

        public async Task<bool> ConnectAsync() {
            var result = await WebClient.LoginAsync();
            if (result.StatusCode == HttpStatusCode.OK) {
                WsClient = new IqOptionWebSocketClient(WebClient.SecuredToken, "iqoption.com");

                if (await WsClient.OpenWebSocketAsync()) Debug.WriteLine("Ws Ready!");

                return true;
            }

            return false;
        }

        public async Task<IqResult<Profile>> GetProfileAsync() {
            var result = await WebClient.GetProfileAsync();
            return result.Content.JsonAs<IqResult<Profile>>();
        }
    }

  

    public class IqOptionConfiguration {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
    }
}