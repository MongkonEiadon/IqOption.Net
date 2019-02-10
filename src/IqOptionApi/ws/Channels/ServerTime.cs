using IqOptionApi.Models;

// ReSharper disable once CheckNamespace
namespace IqOptionApi.ws {
    public partial class IqWsClient
    {
        private ServerTime _serverTime;
        public ServerTime ServerTime
        {
            get => _serverTime;
            private set
            {
                _serverTime = value;
                OnPropertyChanged(nameof(ServerTime));
            }
        }
    }
}