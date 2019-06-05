using System;
using NUnit.Framework;
using System.Threading.Tasks;
using FluentAssertions;
using IqOptionApi.Exceptions;
using IqOptionApi.http.Commands;
using IqOptionApi.Tests.Constants;
using RestSharp;

namespace IqOptionApi.Tests.http
{
    [TestFixture]
    public class GetBalanceModeTest : IqHttpClientBaseTest
    {
        [Test]
        public void GetBalanceMode_WithReturnWithNotAuthorize_ErrorShouldNotThrown()
        {
            MoqHttpClient.Setup(x => x.ExecuteTaskAsync(Any<GetProfileCommand>()))
                .Returns(Task.FromResult(HttpConstants.NoLoggedIn));

            // act
            Action act = () => CreateCut().GetBalanceModeAsync().Wait();

            // assert
            act.Should().Throw<IqOptionMessageExceptionBase>()
                .WithMessage("Not logged in");
        }
    }
}