using System.Threading.Tasks;
using iqoptionapi.models;
using Microsoft.Extensions.Logging;

namespace iqoptionapi.sample {
    public class Startup {
        private readonly IqOptionConfiguration _config;
        private readonly ILogger _logger;
        private readonly ILoggerFactory _loggerFactory;


        public Startup(IqOptionConfiguration config, ILogger<Startup> logger) {
            _config = config;
            _logger = logger;
        }

        public async Task Run() {
            var api = new IqOptionApi(_config.Email, _config.Password);
            _logger.LogInformation($"Connecting to {_config.Host} for {_config.Email}");


            if (await api.ConnectAsync()) {
                _logger.LogInformation("Connect Success");

                //get profile
                var profile = await api.GetProfileAsync();
                _logger.LogInformation($"Success Get Profile for {_config.Email}");

                //get updated instruments
                var instruments = await api.GetInstrumentsAsync();

                //_logger.LogInformation($"Change balance to {profile.Balances[0].Id}");
                //if (await api.ChangeBalanceAsync(profile.Balances[0].Id)) {
                //}
                while (true) {
                    var buyResult = await api.BuyAsync(ActivePair.EURUSD, 1, OrderDirection.Call);

                }

                await api.GetInstrumentsAsync();


            }
            


        }
    }
}