using System;
using System.Threading.Tasks;
using IqOptionApi.Models;

namespace IqOptionApi.Samples.SampleRunners
{
    public class CopyTradeSample : ISampleRunner
    {
        public async Task RunSample()
        {           
            var trader = new IqOptionClient("a@b.com", "changeme");
            var follower = new IqOptionClient("b@c.com", "changeme"); 

            await Task.WhenAll(trader.ConnectAsync(), follower.ConnectAsync());

            // for binary
            trader.WsClient.OpenOptionObservable().Subscribe(x => {
                follower.BuyAsync(x.Active, (int) x.Amount, x.Direction, x.ExpirationTime);
            });

            // for digitals forex and others
            trader.WsClient.OrderChangedObservable().Subscribe(x =>
            {
                if (x.InstrumentType == InstrumentType.DigitalOption)
                    follower.PlaceDigitalOptions(x.OrderChangedEventInfo.InstrumentId, x.OrderChangedEventInfo.Margin);
            });
        }
    }
}