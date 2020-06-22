using System;
using FluentAssertions;
using IqOptionApi.Models;
using IqOptionApi.unit;
using Newtonsoft.Json;
using NUnit.Framework;

namespace IqOptionApi.Tests.JsonTest {
    
    [TestFixture]
    public class HeartBeatTest : TestFor<LoadJsonFileTest>
    {
        private string Json;
        
        [SetUp]
        public void TestSetup() {
            Json = CreateUnit().LoadJson("Json/heartbeat.json");
            Json.Should().NotBeEmpty();

        }


        [Test]
        public void ConvertHeartbeat_WithExistingValue_ValuedConverted() {

            // act
            var result = JsonConvert.DeserializeObject<HeartBeat>(Json);


            // assert
            var dt = DateTimeOffset.FromUnixTimeMilliseconds(1534749247713);

            result.Should().NotBeNull();
            result.Hearbeat.Should().Be(dt);
        }
    }
}