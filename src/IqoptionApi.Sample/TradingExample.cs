using System;
using System.Threading.Tasks;

namespace IqOptionApi.Sample {
    public class TradingExample {
        public async Task RunAsync() {
            var trader = new IqOptionApi("trader@email.com", "passcode");
            var follower = new IqOptionApi("follower@email.com", "passcode");

            await Task.WhenAll(trader.ConnectAsync(), follower.ConnectAsync());

            trader.WsClient.WhenAnyValue(x => x.InfoData)
                .Where(x => x != null && x.Win == WinType.Equal)
                .Subscribe(x => { follower.BuyAsync(x.ActiveId, (int) x.Sum, x.Direction, x.Expired); });


            //var exp = DateTime.Now.AddMinutes(1);
            var exp = DateTime.Now.AddMinutes(1);
            await trader.BuyAsync(ActivePair.EURUSD, 1, OrderDirection.Call, exp);
        }
    }
}