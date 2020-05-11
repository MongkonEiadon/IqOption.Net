using System.Linq;
using System.Threading.Tasks;
using IqOptionApi.Models;

namespace IqOptionApi.Samples.SampleRunners
{
    public class ChangeBalanceSample : SampleRunner
    {

        public override async Task RunSample()
        {
            if (await IqClientApi.ConnectAsync())
            {
                var profile = await IqClientApi.GetProfileAsync();

                var demo = profile.Balances.FirstOrDefault(x => x.Type == BalanceType.Practice);
                await IqClientApi.ChangeBalanceAsync(demo.Id);

                var real = profile.Balances.FirstOrDefault(x => x.Type == BalanceType.Real);
                await IqClientApi.ChangeBalanceAsync(real.Id);
            }
        }
    }
}