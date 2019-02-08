using FluentAssertions;
using IqOptionApi.ws;
using NUnit.Framework;
using Moq;
using TestAutoFixture;
using Websocket.Client;

namespace IqOptionApi.Tests.ws
{
    [TestFixture]
    public class WsClientTest : TestAutoFixtureFor<IqWsClient>
    {
        [Test]
        public void WsClient_InitialInstant_ShouldNotBeNull() {

            // arrange
            var instant = CreateSut();

            // assert
            instant.Should().NotBeNull();
        }
    }

    [TestFixture]
    public class WsClientTestWithMockWebSocket : TestAutoFixtureFor<IqWsClient> {

        [SetUp]
        public void SetUp() {

            Fixture.Customize<IWebsocketClient>(cfg => cfg.FromSeed((x) => new MockWsClient()));
        }

        [Test]
        public void TestCreateMock() {

        }
    }
}
