using IqOptionApi.Models;
using ReactiveUI;

namespace IqOptionApi.ws {
    public partial class IqWsClient {
        private Profile _profile;

        public Profile Profile {
            get => _profile;
            private set =>
                this.RaiseAndSetIfChanged(ref _profile, value);
        }
    }
}