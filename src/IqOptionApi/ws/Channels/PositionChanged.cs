using IqOptionApi.models.CFD;
using ReactiveUI;

// ReSharper disable once CheckNamespace
namespace IqOptionApi.ws {
    public partial class IqWsClient {
        private DigitalInfoData _digitalInfoData;

        public DigitalInfoData DigitalInfoData {
            get => _digitalInfoData;
            private set => this.RaiseAndSetIfChanged(ref _digitalInfoData, value);
        }
    }
}