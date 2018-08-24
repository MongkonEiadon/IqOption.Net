using IqOptionApi.Models;
using RestSharp;

namespace IqOptionApi.http {
    public class LoginV2Request : IqOptionRequest {
        public LoginV2Request(LoginModel loginModel) : base("login", Method.POST) {
            this.AddParameter("email", loginModel.Email);
            this.AddParameter("password", loginModel.Password);
        }
    }
}