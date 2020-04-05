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

            trader.InfoDatasObservable.Select(x => x[0]).Where(x => x.Win == "equal").Subscribe(x => {
                follower.BuyAsync(x.ActiveId, (int) x.Sum, x.Direction, x.Expired);
            });
        }
    }
}