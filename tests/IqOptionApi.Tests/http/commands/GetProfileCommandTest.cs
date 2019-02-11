using FluentAssertions;
using IqOptionApi.http.Commands;
using NUnit.Framework;
using TestAutoFixture;

namespace IqOptionApi.Tests.http.commands {
    [TestFixture]
    public class GetProfileCommandTest : TestAutoFixtureFor<GetProfileCommand> {

        [Test]
        public void GetProfileCommand_UrlMustBe_GetProfile() {
            // act
            var cut = new GetProfileCommand();

            // assert
            cut.Resource.Should().Be("getprofile");
        }
    }
}