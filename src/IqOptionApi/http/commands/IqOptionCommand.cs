using System.Runtime.CompilerServices;
using RestSharp;

[assembly:InternalsVisibleTo("IqOptionApi.Tests.http.commands")]
namespace IqOptionApi.http.Commands {
    internal class IqOptionCommand : RestRequest {
        protected IqOptionCommand(string action, Method method = Method.GET) : base(action, method) {
            AddHeader("Accept", "application/json");
        }
    }
}