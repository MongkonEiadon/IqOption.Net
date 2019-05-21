
using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("IqOptionApi.Tests.http.commands")]
namespace IqOptionApi.http.Commands {
    internal class GetProfileCommand : IqOptionCommand {

        public GetProfileCommand() : base("getprofile") { }
    }
}