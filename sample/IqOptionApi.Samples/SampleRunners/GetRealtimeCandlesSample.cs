using System;
using System.Threading.Tasks;
using IqOptionApi.Models;
using IqOptionApi.Ws;
using Newtonsoft.Json;
using Serilog;

namespace IqOptionApi.Samples.Workers
{
    public class GetCandlesSample : SampleRunner
    {
        /*
         * Purpose:
         *     To get the candles information (Not Realtime) back to the specific
         *     time frame and period
         *
         * Payload Result:
         *     "candles": [
         { "id": 78112508, "from": 1589034889, "at": 1589034890021414406, "to": 1589034890, "open": 1.038268, "close": 1.038268, "min": 1.038268, "max": 1.038268, "volume": 0 },
         { "id": 78112509, "from": 1589034890, "at": 1589034891025205250, "to": 1589034891, "open": 1.038269, "close": 1.038269, "min": 1.038269, "max": 1.038269, "volume": 0 },
                        ]
         */
        public override async Task RunSample()
        {
            if (await IqClientApi.ConnectAsync())
            {
                var candleInfo = await IqClientApi.GetCandlesAsync(ActivePair.EURUSD_OTC, TimeFrame.Min1, 100, DateTimeOffset.Now.AddMinutes(-5));
                _logger.Information(JsonConvert.SerializeObject(candleInfo));
                
            }
        }
    }
}