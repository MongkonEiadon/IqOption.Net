using System;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using iqoptionapi.models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace iqoptionapi.sample {
    public class Startup {
        private readonly IqOptionConfiguration _config;
        private readonly ILogger _logger;
        private readonly ILoggerFactory _loggerFactory;


        public Startup(IOptions<IqOptionConfiguration> config, ILogger<Startup> logger)
        {
            _config = config.Value;
            _logger = logger;
        }

        public async Task RunSample() {

            var api = new IqOptionApi(_config.Email, _config.Password);
            _logger.LogInformation($"Connecting to {_config.Host} for {_config.Email}");


            if (await api.ConnectAsync()) {
                _logger.LogInformation("Connect Success");

                //get profile
                var profile = await api.GetProfileAsync();
                _logger.LogInformation($"Success Get Profile for {_config.Email}");


                // open order EurUsd in smallest period (1min) 
                var exp = DateTime.Now.AddMinutes(1);
                var buyResult = await api.BuyAsync(ActivePair.EURUSD, 1, OrderDirection.Call, exp);


                // get candles data
                var candles = await api.GetCandlesAsync(ActivePair.EURUSD, 1, 100, DateTimeOffset.Now);
                _logger.LogInformation($"Candles received {candles.Count}");

                


            }
            


        }
    }
}