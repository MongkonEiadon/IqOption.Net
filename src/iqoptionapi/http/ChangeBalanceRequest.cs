using RestSharp;

namespace IqOptionApi.http {
    public class ChangeBalanceRequest : IqOptionRequest {
        public ChangeBalanceRequest(long balanceId) : base("profile/changebalance", Method.POST) {
            this.AddParameter("balance_id", balanceId, ParameterType.QueryString);
        }
    }
}