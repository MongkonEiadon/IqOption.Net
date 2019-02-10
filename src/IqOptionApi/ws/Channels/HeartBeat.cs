using IqOptionApi.Models;

namespace IqOptionApi.ws {
    public partial class IqWsClient
    {
        private HeartBeat _heartBeat;
        public HeartBeat HeartBeat
        {
            get => _heartBeat;
            private set
            {
                _heartBeat = value;
                OnPropertyChanged(nameof(HeartBeat));
            }
        }
    }
}