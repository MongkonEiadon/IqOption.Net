using IqOptionApi.Models;
using ReactiveUI;

// ReSharper disable once CheckNamespace
namespace IqOptionApi.ws {
    public partial class IqWsClient {
        private CurrentCandle _currentCandle;

        public CurrentCandle CurrentCandle {
            get => _currentCandle;
            private set => this.RaiseAndSetIfChanged(ref _currentCandle, value);
        }
    }
}