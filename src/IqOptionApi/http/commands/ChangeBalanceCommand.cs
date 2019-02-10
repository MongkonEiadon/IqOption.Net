using RestSharp;

namespace IqOptionApi.http.commands {
    internal class ChangeBalanceCommand : IqOptionCommand {
        public ChangeBalanceCommand(long balanceId) : base("profile/changebalance", Method.POST) {
            this.AddParameter("balance_id", balanceId, ParameterType.QueryString);
        }
    }
}