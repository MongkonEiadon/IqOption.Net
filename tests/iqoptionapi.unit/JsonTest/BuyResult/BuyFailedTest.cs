using System;
using iqoptionapi.ws.result;
using IqOptionApi.Models;
using Newtonsoft.Json;
using Shouldly;
using Xunit;


namespace IqOptionApi.unit.JsonTest
{
    public class BuyFailedTest : IClassFixture<LoadJsonFileTest>
    {
        private readonly LoadJsonFileTest _jsonLoader;

        public readonly string Json;

        public BuyFailedTest(LoadJsonFileTest jsonLoader)
        {
            _jsonLoader = jsonLoader;

            Json = _jsonLoader.LoadJson("BuyResult\\buyFailed.json");
        }

        [Fact]
        public void LoadBuyComplete_WithSuccessResult_DateTimeConverted()
        {

            // act
            var result = JsonConvert.DeserializeObject<BuyCompleteResultMessage>(Json);

            // assert
            result.ShouldNotBeNull();
            result.Message.ShouldNotBeNull();

            var msg = result.Message;
            msg.IsSuccessful.ShouldBeFalse();
            msg.GetMessageDescription().ShouldBe("หมดเวลาสำหรับการซื้อออปชันแล้ว โปรดลองอีกครั้งภายหลัง");

        }


    }
}