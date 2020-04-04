using FluentAssertions;

using iqoptionapi.ws.result;

using Newtonsoft.Json;

using NUnit.Framework;


namespace IqOptionApi.unit.JsonTest
{
    public class BuyFailedTest : TestFor<LoadJsonFileTest>
    {
        private readonly LoadJsonFileTest _jsonLoader;

        public readonly string Json;

        public BuyFailedTest()
        {

            Json = _jsonLoader.LoadJson("BuyResult\\buyFailed.json");
        }

        [Test]
        public void LoadBuyComplete_WithSuccessResult_DateTimeConverted()
        {

            // act
            var result = JsonConvert.DeserializeObject<BuyCompleteResultMessage>(Json);

            // assert
            result.Should().NotBeNull();
            result.Message.Should().NotBeNull();

            var msg = result.Message;
            msg.IsSuccessful.Should().BeFalse();
            msg.GetMessageDescription().Should().Be("หมดเวลาสำหรับการซื้อออปชันแล้ว โปรดลองอีกครั้งภายหลัง");

        }


    }
}