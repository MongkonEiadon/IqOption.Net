using System;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using IqOptionApi.Models;
using IqOptionApi.Models.DigitalOptions;
using IqOptionApi.Ws.Base;

namespace IqOptionApi.Samples.SampleRunners
{
    public class SubscribeLiveDealSample : SampleRunner
    {
        

        public override async Task RunSample()
        {
            
            if (await IqClientApi.ConnectAsync())
            {
                // subscribe to pair to get real-time data for tf1min and tf5min
                IqClientApi.SubscribeLiveDeal("live-deal-digital-option", ActivePair.EURUSD, DigitalOptionsExpiryType.PT1M);
                IqClientApi.SubscribeLiveDeal("live-deal-digital-option", ActivePair.EURUSD, DigitalOptionsExpiryType.PT5M);

                // call the subscribe to listening when mood changed
                IqClientApi.WsClient.LiveDealObservable().Subscribe(x => {

                    // values goes here
                    _logger.Information(
                        $"Lives User Id: {x.UserId} Created {x.CreatedAt} Amount: {x.AmountEnrolled} Direction: {x.InstrumentDir}"
                    );

                });

                // hold 2 secs
                Thread.Sleep(2000);

                // after this line no-more realtime data for min5 print on console
                IqClientApi.UnSubscribeLiveDeal("live-deal-digital-option", ActivePair.EURUSD, DigitalOptionsExpiryType.PT1M);
                IqClientApi.UnSubscribeLiveDeal("live-deal-digital-option", ActivePair.EURUSD, DigitalOptionsExpiryType.PT5M);
            }
        }
    }
}