using System.Runtime.CompilerServices;
using IqOptionApi.Models;
using RestSharp;

[assembly: InternalsVisibleTo("IqOptionApi.Tests.http.commands", AllInternalsVisible = true)]

namespace IqOptionApi.http.Commands {
    internal class LoginV2Command : IqOptionCommand {
        public LoginV2Command(LoginModel loginModel) : base("login", Method.POST) {
            AddParameter("email", loginModel.Email);
            AddParameter("password", loginModel.Password);
        }
    }
}