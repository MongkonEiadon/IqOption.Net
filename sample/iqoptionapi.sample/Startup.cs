using System;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading.Tasks;
using IqOptionApi.Extensions;
using IqOptionApi.Logging;
using IqOptionApi.Models;
using IqOptionApi.Sample.Logging;
using IqOptionApi.ws;
using LogProvider = IqOptionApi.Sample.Logging.LogProvider;

namespace IqOptionApi.Sample {
    public class Startup {

        private readonly ILog _logger = LogProvider.GetCurrentClassLogger();
        private readonly IqOptionConfiguration _config;

        public Startup(IqOptionConfiguration config)
        {
            _config = config;
        }

        public async Task RunSample() {
            
            //var api = new IqOptionApi("mongkon.eiadon@hotmail.com", "Code11054");

            //if (await api.ConnectAsync()) {
            //    _logger.Info("Connect Success");

            //    //get profile
            //    var profile = await api.GetProfileAsync();
            //    _logger.Info($"Success Get Profile for {_config.Email}");


            //    // open order EurUsd in smallest period (1min) 
            //    var exp = DateTime.Now.AddMinutes(1);
            //    var buyResult = await api.BuyAsync(ActivePair.EURUSD, 1, OrderDirection.Call, exp);


            //    // get candles data
            //    var candles = await api.GetCandlesAsync(ActivePair.EURUSD, TimeFrame.Min1, 100, DateTimeOffset.Now);
            //    _logger.Info($"CandleCollections received {candles.Count}");

                
            //    // subscribe to pair to get real-time data for tf1min and tf5min
            //    var streamMin1 = await api.SubscribeRealtimeDataAsync(ActivePair.EURUSD, TimeFrame.Min1);
            //    var streamMin5 = await api.SubscribeRealtimeDataAsync(ActivePair.EURUSD, TimeFrame.Min5);

            //    streamMin5.Merge(streamMin1)
            //        .Subscribe(candleInfo => {
            //            _logger.Info($"Now {ActivePair.EURUSD} {candleInfo.TimeFrame} : Bid={candleInfo.Bid}\t Ask={candleInfo.Ask}\t");
            //    });

            //    // after this line no-more realtime data for min5 print on console
            //    await api.UnSubscribeRealtimeData(ActivePair.EURUSD, TimeFrame.Min5);

               
            //    //when price down with 

            //}
            


        }
    }
    

    public class IqClientExample {


        public IIqOptionClient api;

        public IqClientExample() {
             api = new IqOptionClient("mongkon.eiadon@hotmail.com", "Code11054");
        }

        public async Task RunAsync() {

            try {

                //logging in
                await api.ConnectAsync();

                api.WsClient.ToObservable(x => x.ServerTime).Subscribe(x => {

                    Console.WriteLine(x.Message.ToLocalTime());

                });


            }
            catch (Exception ex) {

            }


            return;
        }
        
    }
}