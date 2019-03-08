using System;
using System.IO;
using System.Reactive.Linq;
using System.Threading.Tasks;
using IqOptionApi.Models;
using Newtonsoft.Json;
using ReactiveUI;

namespace IqOptionApi.Sample {
    public class TradingExample {
        public async Task RunAsync()
        {

            var traderAccount = File.ReadAllText("account.json");
            var account = JsonConvert.DeserializeObject<account>(traderAccount);


            var trader = new IqOptionApi(account.trader.username, account.trader.password);
            var follower = new IqOptionApi(account.follower.username, account.follower.password);

            await Task.WhenAll(trader.ConnectAsync(), follower.ConnectAsync());

            trader.WsClient.WhenAnyValue(x => x.InfoData)
                .Where(x => x != null && x.Win == WinType.Equal)
                .Subscribe(x => { follower.BuyAsync(x.ActiveId, (int) x.Sum, x.Direction, x.Expired); });


            //var exp = DateTime.Now.AddMinutes(1);
            var exp = DateTime.Now.AddMinutes(1);
            await trader.BuyAsync(ActivePair.EURUSD, 1, OrderDirection.Call, exp);
        }
    }

    public class account
    {
        public Trader trader { get; set; }
        public Follower follower { get; set; }
    }

    public class Trader
    {
        public string username { get; set; }
        public string password { get; set; }
    }

    public class Follower
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}