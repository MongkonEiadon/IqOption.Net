using iqoptionapi.models;
using Newtonsoft.Json;
using Shouldly;
using Xunit;

namespace iqoptionapi.unit.JsonTest {
    public class HeartBeatTest : IClassFixture<LoadJsonFileTest>
    {
        private readonly LoadJsonFileTest _loadTest;
        private string Json => _loadTest.LoadJson("heartbeat.json");
        public HeartBeatTest(LoadJsonFileTest loadTest) {
            _loadTest = loadTest;
        }


        [Fact]
        public void ConvertHeartbeat_WithExistingValue_ValuedConverted() {

            // act
            var result = JsonConvert.DeserializeObject<HeartBeat>(Json);


            // assert
            result.ShouldNotBeNull();
            result.Message.ShouldBe(1534749247713);
            result.HearBeatDateTime.ShouldNotBeNull();
        }
    }
}