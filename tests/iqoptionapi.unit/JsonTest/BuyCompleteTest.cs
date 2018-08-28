using System;
using iqoptionapi.ws.result;
using IqOptionApi.Models;
using Newtonsoft.Json;
using Shouldly;
using Xunit;

namespace IqOptionApi.unit.JsonTest
{
    public class BuyCompleteTest : IClassFixture<LoadJsonFileTest> {
        private readonly LoadJsonFileTest _jsonLoader;

        public readonly string Json;

        public BuyCompleteTest(LoadJsonFileTest jsonLoader) {
            _jsonLoader = jsonLoader;

            Json = _jsonLoader.LoadJson("buycomplete.json");
        }


        [Fact]
        public void LoadBuyComplete_WithSuccessResult_DateTimeConverted() {

            // act
            var result = JsonConvert.DeserializeObject<BuyCompleteResultMessage>(Json);

            // assert
            result.ShouldNotBeNull();
            result.Message.ShouldNotBeNull();

            var msg = result.Message;
            msg.IsSuccessful.ShouldBeTrue();
            msg.GetMessageDescription().ShouldBe("Successful");

            var buy = msg.Result;

            buy.Created.ShouldNotBeNull();
            buy.Exp.ShouldNotBeNull();

            buy.UserId.ShouldBe(1234);
        }

    }
}