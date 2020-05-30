using System;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using IqOptionApi.Models;
using IqOptionApi.Ws;
using IqOptionApi.Ws.Request;

namespace IqOptionApi.Samples.SampleRunners
{
    public class SubscribeRealtimeCandlesSample : SampleRunner
    {
        public override async Task RunSample()
        {
            if (await IqClientApi.ConnectAsync())
            {
                // subscribe to pair to get real-time data for tf1min and tf5min
                var streamMin1 = await IqClientApi.SubscribeRealtimeQuoteAsync(ActivePair.EURUSD_OTC, TimeFrame.Min1);
                var streamMin5 = await IqClientApi.SubscribeRealtimeQuoteAsync(ActivePair.EURUSD_OTC, TimeFrame.Min5);

                streamMin5.Merge(streamMin1)
                    .Subscribe(candleInfo =>
                    {
                        _logger.Information(
                            $"Now {ActivePair.EURUSD_OTC} {candleInfo.TimeFrame} : Bid={candleInfo.Bid}\t Ask={candleInfo.Ask}\t");
                    });
                
                
                // hold 2 secs
                Thread.Sleep(2000);
                
                // after this line no-more realtime data for min5 print on console
                await IqClientApi.UnSubscribeRealtimeData(ActivePair.EURUSD_OTC, TimeFrame.Min5);
            }
        }
    }
}