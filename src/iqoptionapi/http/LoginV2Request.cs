using iqoptionapi.models;
using RestSharp;

namespace iqoptionapi.http {
    public class LoginV2Request : IqOptionRequest {
        public LoginV2Request(LoginModel loginModel) : base("login", Method.POST) {
            this.AddParameter("email", loginModel.Email);
            this.AddParameter("password", loginModel.Password);
        }
    }
}