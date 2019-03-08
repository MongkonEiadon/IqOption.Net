using System.Threading.Tasks;

using FluentAssertions;
using IqOptionApi.Exceptions;
using IqOptionApi.http;
using IqOptionApi.Models;
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
            MockHttpClient.Setup(x => x.ChangeBalance(1234))
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
            MockHttpClient.Setup(x => x.ChangeBalance(1234))
                .Throws(new IqOptionMessageExceptionBase(null));

            // act
            var result = await CreateCut().ChangeBalanceAsync(1234);

            // assert
            result.Should().BeFalse();
        }

        #endregion

        #region GetProfile

        [Test]
        public void GetProfile_WithCorrectResult_ProfileMustReturned()
        {
            // arrange
            var profile = A<Profile>();
            MockHttpClient.Setup(x => x.GetProfileAsync())
                .ReturnsAsync(profile);

            // act
            var result = CreateCut().GetProfileAsync();

            // assert
            result.Should().NotBeNull();
        }

        #endregion


    }

}