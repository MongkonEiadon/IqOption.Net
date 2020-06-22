using System.Threading.Tasks;
using Serilog;

namespace IqOptionApi.Samples.SampleRunners
{
    public interface ISampleRunner
    {
        Task RunSample();
    }

    public abstract class SampleRunner : ISampleRunner
    {
        protected readonly ILogger _logger = LogHelper.Log;
        protected IqOptionClient IqClientApi = new IqOptionClient("adventuretimelaz11@thairiches.com", "0123456789");

        public abstract Task RunSample();
    }
}