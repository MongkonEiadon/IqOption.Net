using System;
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
        protected IqOptionClient IqClientApi = new IqOptionClient(
            Environment.GetEnvironmentVariable("IqOptionUserName"),
            Environment.GetEnvironmentVariable("IqOptionPassword"));

        public abstract Task RunSample();
    }
}