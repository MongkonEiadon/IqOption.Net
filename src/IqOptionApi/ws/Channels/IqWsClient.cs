using IqOptionApi.Models;

// ReSharper disable once CheckNamespace
namespace IqOptionApi.ws {
    public partial class IqWsClient
    {

        private InfoData _infoData;
        public InfoData InfoData
        {
            get => _infoData;
            private set
            {
                _infoData = value;
                OnPropertyChanged(nameof(InfoData));
            }
        }
    }
}