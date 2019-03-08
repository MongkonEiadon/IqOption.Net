using RestSharp;

namespace IqOptionApi.http.Commands {
    public class ChangeBalanceCommand : IqOptionCommand {
        public ChangeBalanceCommand(long balanceId) : base("profile/changebalance", Method.POST) {
            AddParameter("balance_id", balanceId, ParameterType.QueryString);
        }
    }
}