using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Serilog;

namespace IqOptionApi.Samples.Workers
{
    public class GetProfileSample : SampleRunner
    {


        public override async Task RunSample()
        {
            if (await IqClientApi.ConnectAsync())
            {
                var profile = await IqClientApi.GetProfileAsync();
                _logger.Information(JsonConvert.SerializeObject(profile));
            }
        }
    }
}