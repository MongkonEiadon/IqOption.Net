using System.Runtime.CompilerServices;
using IqOptionApi.Models;
using RestSharp;

[assembly:InternalsVisibleTo("IqOptionApi.Tests.http.commands", AllInternalsVisible = true)]
namespace IqOptionApi.http.commands {
    public class LoginV2Command : IqOptionCommand {
        public LoginV2Command(LoginModel loginModel) : base("login", Method.POST) {
            this.AddParameter("email", loginModel.Email);
            this.AddParameter("password", loginModel.Password);
        }
    }
}