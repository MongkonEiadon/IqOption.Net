using System;
using System.Threading.Tasks;
using IqOptionApi.Logging;
using IqOptionApi.Models;

namespace IqOptionApi.Sample {
    internal class Program {
        private static IIqOptionClient api;

        private static void Main(string[] args) {

            LogProvider.SetCurrentLogProvider(new ColoredConsoleLogProvider());
            MainAsync().Wait();
        }

        private static async Task MainAsync() {
            api = new IqOptionClient("mongkon.eiadon@hotmail.com", "Code11054");

            try {
                //logging in
                if (await api.ConnectAsync()) {
                    //get profile
                    var profile = await api.HttpClient.GetProfileAsync();


                    // open order EurUsd in smallest period (1min) 
                    var exp = DateTime.Now.AddMinutes(1);
                    var buyResult = await api.BuyAsync(ActivePair.EURUSD, 1, OrderDirection.Call, exp);


                    // get candles data
                    var candle = await api.GetCandlesAsync(ActivePair.EURUSD, TimeFrame.Min1, 100, DateTimeOffset.Now);
                    api.WsClient.ChannelMessage
                        .Subscribe(x => { Console.WriteLine(x);});


                    // subscribe to pair to get real-time data for tf1min and tf5min
                    //await api.SubscribeCandlesAsync(ActivePair.EURUSD, TimeFrame.Min1);
                    //await api.SubscribeCandlesAsync(ActivePair.EURUSD, TimeFrame.Min5);

                    //api.CandleInfo
                    //    .Subscribe(candleInfo => {
                    //        Console.WriteLine(
                    //            $"Now {ActivePair.EURUSD} {candleInfo.TimeFrame} : Bid={candleInfo.Bid}\t Ask={candleInfo.Ask}\t");
                    //    });

                    //// after this line no-more realtime data for min5 print on console
                    //await api.UnsubscribeCandlesAsync(ActivePair.EURUSD, TimeFrame.Min5);
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
            finally {
                Console.ReadLine();
            }
        }
    }
}