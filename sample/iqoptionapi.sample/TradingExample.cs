using System;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using IqOptionApi;
using IqOptionApi.Models;
using Serilog;

namespace IqOptionApi.Sample {
    public class TradingExample {

        public TradingExample()
        {
        }

        public async Task RunSample() {

            var trader = new IqOptionApi("mongkon.eiadon@gmail.com", "Code11054");
            var follower = new IqOptionApi("liie.m@excelbangkok.com", "Code11054");

            await Task.WhenAll(trader.ConnectAsync(), follower.ConnectAsync());

            trader.InfoDatasObservable.Select(x => x[0]).Where(x => x.Win == "equal").Subscribe(x => {
                follower.BuyAsync(x.ActiveId, (int) x.Sum, x.Direction, x.Expired);
            });

        }
    }
}