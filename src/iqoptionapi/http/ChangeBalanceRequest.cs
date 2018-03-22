using RestSharp;

namespace iqoptionapi.http {
    public class ChangeBalanceRequest : IqOptionRequest {

        public ChangeBalanceRequest(long balanceId) : base("profile/ChangeBalance", Method.POST) {
            this.AddParameter("balance_id", balanceId);
        }
    }
}