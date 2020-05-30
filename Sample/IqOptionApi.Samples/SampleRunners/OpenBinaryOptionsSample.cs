using System;
using System.Threading.Tasks;
using IqOptionApi.Models;

namespace IqOptionApi.Samples.SampleRunners
{
    public class OpenBinaryOptionsSample : SampleRunner
    {
        public override async Task RunSample()
        {
            if (await IqClientApi.ConnectAsync())
            {
                var result = await IqClientApi.BuyAsync(ActivePair.EURUSD_OTC, 1, OrderDirection.Call, DateTimeOffset.MinValue);

                Console.WriteLine($"PositionId = {result.PositionId}");
            }
        }
    }
}