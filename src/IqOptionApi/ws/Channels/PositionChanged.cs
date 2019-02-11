using IqOptionApi.models.CFD;

// ReSharper disable once CheckNamespace
namespace IqOptionApi.ws {
    public partial class IqWsClient {
        private DigitalInfoData _digitalInfoData;

        public DigitalInfoData DigitalInfoData {
            get => _digitalInfoData;
            private set {
                _digitalInfoData = value;
                OnPropertyChanged(nameof(DigitalInfoData));
            }
        }
    }
}