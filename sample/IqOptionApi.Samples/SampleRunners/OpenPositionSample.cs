using System;
using System.Threading.Tasks;
using IqOptionApi.Models;
using Serilog;

namespace IqOptionApi.Samples.Workers
{
    public class OpenPositionSample : SampleRunner
    {
        
        public override async Task RunSample()
        {
            if (await IqClientApi.ConnectAsync())
            {                // open order EurUsd in smallest period (1min) 
                var exp = DateTime.Now.AddMinutes(1);
                var buyResult = await IqClientApi.BuyAsync(ActivePair.EURUSD, 1, OrderDirection.Call, exp);
            }
        }
    }
}