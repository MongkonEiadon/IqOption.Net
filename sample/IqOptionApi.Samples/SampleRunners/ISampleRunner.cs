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
        protected IqOptionClient IqClientApi = new IqOptionClient("username@email.com", "yourpassword");

        public abstract Task RunSample();
    }
}