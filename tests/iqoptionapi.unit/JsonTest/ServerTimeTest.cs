using System;
using IqOptionApi.Models;
using Newtonsoft.Json;
using Shouldly;
using Xunit;

namespace IqOptionApi.unit.JsonTest {
    public class ServerTimeTest : IClassFixture<LoadJsonFileTest> {
        private readonly LoadJsonFileTest _loader;


        private string Json { get;  }
        public ServerTimeTest(LoadJsonFileTest loader) {
            _loader = loader;

            Json = _loader.LoadJson("timesync.json");
        }


        [Fact]
        public void GetTimeSync_WithExistingValue_DateTimeOffsetConvertedCorrected() {

            // act
            var result = JsonConvert.DeserializeObject<ServerTime>(Json);


            // assert
            var dt = DateTimeOffset.FromUnixTimeMilliseconds(1534749220468);

            result.ShouldNotBeNull();
            result.Message.ShouldBe(dt);
        }
    }
}