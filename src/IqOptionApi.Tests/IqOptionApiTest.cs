using System.Threading.Tasks;

using FluentAssertions;

using IqOptionApi.http;
using IqOptionApi.ws;

using Moq;

using NUnit.Framework;
using TestAutoFixture;

namespace IqOptionApi.Tests {

    [TestFixture]
    public class IqOptionApiTest : TestAutoFixtureFor<IqOptionApi> {

        private Mock<IIqWsClient> MockWsClient { get; set; }
        private Mock<IIqHttpClient> MockHttpClient { get; set; }

        [SetUp]
        public void Setup() {
            MockHttpClient = InjectMock<IIqHttpClient>();
            MockWsClient = InjectMock<IIqWsClient>();

            Fixture.Customize<IqOptionApi>(cfg =>
                cfg.FromFactory(() => new IqOptionApi("","") {
                    WsClient = MockWsClient.Object,
                    HttpClient = MockHttpClient.Object
                }));
        }

        #region ChangeBalance

        [Test]  
        public async Task ChangeBalance_WithSuccessFul_TrueReturned() {

            //arrange
            Fixture.Customize<IqHttpResult<IHttpResultMessage>>(cfg => cfg.With(x => x.IsSuccessful, true));
            MockHttpClient.Setup(x => x.ChangeBalanceAsync(1234))
                .Returns(Task.FromResult(A<IqHttpResult<IHttpResultMessage>>()));

            // act
            var result = await CreateCut().ChangeBalanceAsync(1234);

            // assert
            result.Should().BeTrue();
        }


        [Test]
        public async Task ChangeBalance_WithNotSuccess_FalseReturned()
        {

            //arrange
            Fixture.Customize<IqHttpResult<IHttpResultMessage>>(cfg => cfg.With(x => x.IsSuccessful, false));
            MockHttpClient.Setup(x => x.ChangeBalanceAsync(1234))
                .Returns(Task.FromResult(A<IqHttpResult<IHttpResultMessage>>()));

            // act
            var result = await CreateCut().ChangeBalanceAsync(1234);

            // assert
            result.Should().BeFalse();
        }

        #endregion


    }

}