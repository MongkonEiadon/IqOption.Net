using NUnit.Framework;
using System.Threading.Tasks;
using IqOptionApi.http.Commands;
using IqOptionApi.Tests.Constants;
using RestSharp;

namespace IqOptionApi.Tests.http
{
    [TestFixture]
    public class GetBalanceModeTest : IqHttpClientBaseTest
    {
        [Test]
        public async Task GetBalanceMode_WithReturnWithNotAuthorize_ErrorShouldNotThrown()
        {
            MoqHttpClient.Setup(x => x.ExecuteTaskAsync(Any<GetProfileCommand>()))
                .Returns(Task.FromResult(HttpConstants.NoLoggedIn));

            // act
            var api = await CreateCut().GetBalanceModeAsync();
        }
    }
}