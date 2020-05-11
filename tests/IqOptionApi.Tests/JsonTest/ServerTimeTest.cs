using System;
using FluentAssertions;
using IqOptionApi.Models;
using IqOptionApi.unit;
using Newtonsoft.Json;
using NUnit.Framework;

namespace IqOptionApi.Tests.JsonTest {
    
    [TestFixture]
    public class ServerTimeTest : TestFor<LoadJsonFileTest> {


        private string Json { get;  }
        public ServerTimeTest() {

            Json = CreateUnit().LoadJson("timesync.json");
        }


        [Test]
        public void GetTimeSync_WithExistingValue_DateTimeOffsetConvertedCorrected() {

            // act
            var result = JsonConvert.DeserializeObject<ServerTime>(Json);


            // assert
            var dt = DateTimeOffset.FromUnixTimeMilliseconds(1534749220468);

            result.Should().NotBeNull();
            result.Message.Should().Be(dt);
        }
    }
}