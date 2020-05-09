using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using IqOptionApi.Models;
using Serilog;

namespace IqOptionApi.Samples.Workers
{
    public class SubscribeRealtimeCandlesSample : ISampleRunner
    {
        
        private readonly ILogger _logger = LogHelper.Log;
        
        public Task RunSample()
        {
            
            var api = new IqOptionClient("mongkon.eiadon@hotmail.com", "Code11054");

            if (await api.ConnectAsync())
            {
                // subscribe to pair to get real-time data for tf1min and tf5min
                var streamMin1 = await api.SubscribeRealtimeDataAsync(ActivePair.EURUSD, TimeFrame.Min1);
                var streamMin5 = await api.SubscribeRealtimeDataAsync(ActivePair.EURUSD, TimeFrame.Min5);

                streamMin5.Merge(streamMin1)
                    .Subscribe(candleInfo =>
                    {
                        _logger.Information(
                            $"Now {ActivePair.EURUSD} {candleInfo.TimeFrame} : Bid={candleInfo.Bid}\t Ask={candleInfo.Ask}\t");
                    });
                
                // after this line no-more realtime data for min5 print on console
                await api.UnSubscribeRealtimeData(ActivePair.EURUSD, TimeFrame.Min5);
            }
        }
    }
}