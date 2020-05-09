using System.Threading.Tasks;
using Newtonsoft.Json;

namespace IqOptionApi.Samples.SampleRunners
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