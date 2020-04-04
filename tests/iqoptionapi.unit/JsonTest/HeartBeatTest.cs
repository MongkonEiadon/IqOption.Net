
using System;

using FluentAssertions;

using IqOptionApi.Models;
using Newtonsoft.Json;

using NUnit.Framework;

namespace IqOptionApi.unit.JsonTest {
    
    [TestFixture]
    public class HeartBeatTest : TestFor<LoadJsonFileTest>
    {
        private readonly LoadJsonFileTest _loadTest;
        private string Json => _loadTest.LoadJson("heartbeat.json");
        public HeartBeatTest(LoadJsonFileTest loadTest) {
            _loadTest = loadTest;
        }


        [Test]
        public void ConvertHeartbeat_WithExistingValue_ValuedConverted() {

            // act
            var result = JsonConvert.DeserializeObject<HeartBeat>(Json);


            // assert

            var dt = DateTimeOffset.FromUnixTimeMilliseconds(1534749247713);

            result.Should().NotBeNull();
            result.Message.Should().Be(dt);
        }
    }
}