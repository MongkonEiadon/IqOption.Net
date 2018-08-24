using IqOptionApi.Models;
using IqOptionApi.ws;
using Newtonsoft.Json;
using Shouldly;
using Xunit;

namespace IqOptionApi.unit.JsonTest {
    public class CurrentCandleTest : IClassFixture<LoadJsonFileTest> {
        public CurrentCandleTest(LoadJsonFileTest loadJsonFileTest) {
            _loadJsonFileTest = loadJsonFileTest;

            Json = _loadJsonFileTest.LoadJson("subscribeMessage\\candle-generated.json");
        }

        private readonly LoadJsonFileTest _loadJsonFileTest;

        private string Json { get; }

        [Fact]
        public void GetCandlesResult_WithFromAndTo_DateTimeMustSetCorrectly() {

            //act
            var result = JsonConvert.DeserializeObject<CurrentCandleInfoResultMessage>(Json);

            // assert
            result.ShouldNotBeNull();

            var candles = result.Message;
            candles.ShouldNotBeNull();
            candles.ActivePair.ShouldBe(ActivePair.EURUSD);

            candles.Max.ShouldBe(1.14107);
            candles.Min.ShouldBe(1.141035);

            candles.Bid.ShouldBe(1.14099);
            candles.Ask.ShouldBe(1.14115);

            candles.Open.ShouldBe(1.141055);
            candles.Close.ShouldBe(1.14107);

            candles.From.Year.ShouldBe(2018);
            candles.From.Month.ShouldBe(08);
            candles.From.Day.ShouldBe(20);


            candles.To.Year.ShouldBe(2018);
            candles.To.Month.ShouldBe(08);
            candles.To.Day.ShouldBe(20);


        }
    }
}