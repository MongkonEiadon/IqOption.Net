using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace iqoptionapi.sample {
    public class Startup {
        private readonly IqOptionConfiguration _config;
        private readonly ILogger _logger;


        public Startup(IqOptionConfiguration config, ILogger<Startup> logger) {
            _config = config;
            _logger = logger;
        }

        public async Task Run() {
            var api = new IqOptionApi(_config.Email, _config.Password);

            if (await api.ConnectAsync()) {
                _logger.LogInformation("Connect Success");

                //get profile
                var profile = await api.GetProfileAsync();
                _logger.LogInformation($"Success Get Profile for {profile.UserProfile.Email}");
            }


        }
    }
}