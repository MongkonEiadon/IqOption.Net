using System;
using System.Threading.Tasks;

namespace IqOptionApi.Samples.SampleRunners
{
    public class CopyTradeSample : ISampleRunner
    {
        public async Task RunSample()
        {           
            var trader = new IqOptionClient("a@b.com", "changeme");
            var follower = new IqOptionClient("b@c.com", "changeme"); 

            await Task.WhenAll(trader.ConnectAsync(), follower.ConnectAsync());

            trader.WsClient.OpenOptionObservable().Subscribe(x => {
                follower.BuyAsync(x.Active, (int) x.Amount, x.Direction, x.ExpirationTime);
            });
        }
    }
}