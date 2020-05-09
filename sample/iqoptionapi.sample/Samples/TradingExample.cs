using System;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using IqOptionApi;
using IqOptionApi.Models;
using Serilog;

namespace IqOptionApi.Sample {
    public class TradingExample {
        public async Task RunSample() {

            var trader = new IqOptionClient("a@b.com", "changeme");
            var follower = new IqOptionClient("b@c.com", "changeme"); 

            await Task.WhenAll(trader.ConnectAsync(), follower.ConnectAsync());

            trader.WsClient.OpenOptionObservable().Subscribe(x => {
                follower.BuyAsync(x.Active, (int) x.Amount, x.Direction, x.ExpirationTime);
            });
        }
    }

    public class GetProfileExample 
    {
        public async Task RunAsync()
        {
            
        }
    }
}