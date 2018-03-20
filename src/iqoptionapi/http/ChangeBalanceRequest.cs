using RestSharp;

namespace iqoptionapi.http {
    public class ChangeBalanceRequest : IqOptionRequest {

        public ChangeBalanceRequest(long balanceid) : base("profile/changebalance", Method.POST) {
            this.AddParameter("balance_id", balanceid);
        }
    }
}