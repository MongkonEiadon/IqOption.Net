using RestSharp;

namespace IqOptionApi.http.Commands {
    internal class ChangeBalanceCommand : IqOptionCommand {
        public ChangeBalanceCommand(long balanceId) : base("profile/changebalance", Method.POST) {
            AddParameter("balance_id", balanceId, ParameterType.QueryString);
        }
    }
}