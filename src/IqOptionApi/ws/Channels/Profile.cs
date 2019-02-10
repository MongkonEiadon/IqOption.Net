using IqOptionApi.Models;

namespace IqOptionApi.ws {
    public partial class IqWsClient {

        private Profile _profile;
        public Profile Profile
        {
            get => _profile;
            private set {
                _profile = value;
                OnPropertyChanged(nameof(Profile));
            }
        }
    }
}