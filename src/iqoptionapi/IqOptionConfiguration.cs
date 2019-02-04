
using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("IqOptionApi.Unit")]
namespace IqOptionApi {
    public class IqOptionConfiguration {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
    }
}