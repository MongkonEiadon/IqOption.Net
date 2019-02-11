using RestSharp;

namespace IqOptionApi.http.Commands {
    public class IqOptionCommand : RestRequest {
        protected IqOptionCommand(string action, Method method = Method.GET) : base(action, method) {
            AddHeader("Accept", "application/json");
        }
    }
}