﻿using FluentAssertions;
using IqOptionApi.ws;
using NUnit.Framework;
using TestAutoFixture;

namespace IqOptionApi.Tests.ws {
    [TestFixture]
    public class WsClientTest : TestAutoFixtureFor<IqWsClient> {
        [Test]
        public void WsClient_InitialInstant_ShouldNotBeNull() {
            // arrange
            var instant = CreateCut();

            // assert
            instant.Should().NotBeNull();
        }
    }

    [TestFixture]
    [Category("Integrations")]
    public class WsClientIntegrationTest : TestAutoFixtureFor<IqWsClient> { }
}