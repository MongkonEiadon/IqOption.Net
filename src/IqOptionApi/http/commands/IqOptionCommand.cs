using RestSharp;

namespace IqOptionApi.http.commands {
    public class IqOptionCommand : RestRequest {
        protected IqOptionCommand(string action, Method method = Method.GET) : base(action, method) {
            this.AddHeader("Accept", "application/json");
        }
    }
}