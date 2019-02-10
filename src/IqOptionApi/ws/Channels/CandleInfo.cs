using IqOptionApi.Models;

// ReSharper disable once CheckNamespace
namespace IqOptionApi.ws {
    public partial class IqWsClient
    {
        private CurrentCandle _currentCandle;
        public CurrentCandle CurrentCandle
        {
            get => _currentCandle;
            private set
            {
                _currentCandle = value;
                OnPropertyChanged(nameof(CurrentCandle));
            }
        }
    }
}