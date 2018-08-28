using System;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using IqOptionApi;
using IqOptionApi.Models;
using Serilog;

namespace iqoptionapi.sample {
    public class Startup {
        private readonly IqOptionConfiguration _config;
        private readonly ILogger _logger;

        public Startup(IqOptionConfiguration config, Serilog.ILogger logger)
        {
            _config = config;
            _logger = logger;
        }

        public async Task RunSample() {

            var api = new IqOptionApi.IqOptionApi(_config.Email, _config.Password);
            _logger.Information($"Connecting to {_config.Host} for {_config.Email}");


            if (await api.ConnectAsync()) {
                _logger.Information("Connect Success");

                //get profile
                var profile = await api.GetProfileAsync();
                _logger.Information($"Success Get Profile for {_config.Email}");


                // received when expiration occured
                api.InfoDatasObservable.Select(x => x.FirstOrDefault()).Subscribe(x => {
                    _logger.Information($"Buy result is {x.Win} with {x.ActiveId}");
                });

                // open order EurUsd in smallest period (1min) 
                var exp = DateTime.Now.AddMinutes(1);
                var buyResult = await api.BuyAsync(ActivePair.EURUSD, 1, OrderDirection.Call, exp);


                // get candles data
                var candles = await api.GetCandlesAsync(ActivePair.EURUSD, TimeFrame.Min1, 100, DateTimeOffset.Now);
                _logger.Information($"CandleCollections received {candles.Count}");

                
                // subscribe to pair to get real-time data for tf1min and tf5min
                var streamMin1 = await api.SubscribeRealtimeDataAsync(ActivePair.EURUSD, TimeFrame.Min1);
                var streamMin5 = await api.SubscribeRealtimeDataAsync(ActivePair.EURUSD, TimeFrame.Min5);

                streamMin5.Merge(streamMin1)
                    .Subscribe(candleInfo => {
                        _logger.Information($"Now {ActivePair.EURUSD} {candleInfo.TimeFrame} : Bid={candleInfo.Bid}\t Ask={candleInfo.Ask}\t");
                });

                // after this line no-more realtime data for min5 print on console
                await api.UnSubscribeRealtimeData(ActivePair.EURUSD, TimeFrame.Min5);

               
                //when price down with 

            }
            


        }
    }
}