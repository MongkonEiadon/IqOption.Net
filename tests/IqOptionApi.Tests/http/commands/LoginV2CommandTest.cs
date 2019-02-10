using System.Linq;
using System.Net.Http;
using FluentAssertions;
using IqOptionApi.http.commands;
using IqOptionApi.Models;
using NUnit.Framework;
using RestSharp;
using TestAutoFixture;

namespace IqOptionApi.Tests.http.commands
{
    [TestFixture]
    public class LoginV2CommandTest : TestAutoFixtureFor<LoginV2Command>
    {
        [SetUp]
        public void SetUp() {
            Fixture.Customize<LoginModel>(cfg =>
                cfg.With(x => x.Email, "email")
                    .With(x => x.Password, "p@ssw0rd"));
        }

        [Test]
        public void VerifyEmailParameter_Email() {

            // act
            var cut = new LoginV2Command(A<LoginModel>());

            // assert
            cut.Parameters.FirstOrDefault(x => x.Name == "email")?.Value.Should().Be("email");
        }


        [Test]
        public void VerifyEmailParameter_Password()
        {

            // act
            var cut = new LoginV2Command(A<LoginModel>());

            // assert
            cut.Parameters.FirstOrDefault(x => x.Name == "password")?.Value.Should().Be("p@ssw0rd");
        }
        [Test]
        public void VerifyEmailParameter_Method_ShouldBePUT()
        {

            // act
            var cut = new LoginV2Command(A<LoginModel>());

            // assert
            cut.Method.Should().Be(Method.POST);
        }
    }
}
