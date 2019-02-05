
using System;
using FluentAssertions;
using IqOptionApi.Models;
using Newtonsoft.Json;
using NUnit.Framework;

namespace IqOptionApi.Tests.JsonTest {

    [TestFixture]
    public class HeartBeatTest : LoadJsonFileTest<HeartBeat> 
    {
        public override string JsonSourceFileName => "heartbeat.json";
        

        [Test]
        public void ConvertHeartbeat_WithExistingValue_ValuedConverted() {

            // arrange
            var dt = DateTimeOffset.FromUnixTimeMilliseconds(1534749247713);
           
            // act
            var result = ReadFileSource();
            
            // assert
            result.Should().NotBeNull();
            result.Message.Should().Be(dt);
        }
    }
}