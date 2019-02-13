using IqOptionApi.Models;
using ReactiveUI;

// ReSharper disable once CheckNamespace
namespace IqOptionApi.ws {
    public partial class IqWsClient {
        private InfoData _infoData;

        public InfoData InfoData {
            get => _infoData;
            private set => this.RaiseAndSetIfChanged(ref _infoData, value);
        }
    }
}