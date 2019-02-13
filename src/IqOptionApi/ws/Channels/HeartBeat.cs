using IqOptionApi.Models;
using ReactiveUI;

namespace IqOptionApi.ws {
    public partial class IqWsClient {
        private HeartBeat _heartBeat;

        public HeartBeat HeartBeat {
            get => _heartBeat;
            private set => this.RaiseAndSetIfChanged(ref _heartBeat, value);
        }
    }
}