using System;
using System.Linq;
using FluentAssertions;
using IqOptionApi.unit;
using IqOptionApi.Ws;
using Newtonsoft.Json;
using NUnit.Framework;

namespace IqOptionApi.Tests.JsonTest {
    public class GetCandlesResultTest : TestFor<LoadJsonFileTest>
    {

        private string Json;
        
        [SetUp]
        public void TestSetup() {

            Json = CreateUnit().LoadJson("Json/Candles/GetCandles_Success.json");
        }
        

        [Test]
        public void GetCandlesResult_CandlesItems_ResultMustReturnSuccess() {

            //act
            var result = JsonConvert.DeserializeObject<GetCandleItemsResultMessage>(Json);


            //assert
            result.Message.Infos.Should().NotBeEmpty();

            var candles = result.Message.Infos.FirstOrDefault(x => x.Id == 17990335);
            candles.Should().NotBeNull();
            candles.Open.Should().Be(1.14384);
            candles.Close.Should().Be(1.143865);
            candles.Min.Should().Be(1.14384);
            candles.Max.Should().Be(1.143865);
        }

        [Test]
        public void GetCandlesResult_WithFromAndTo_DateTimeMustSetCorrectly() {

            //act
            var result = JsonConvert.DeserializeObject<GetCandleItemsResultMessage>(Json);


            //assert
            result.Message.Infos.Should().NotBeEmpty();

            var candles = result.Message.Infos.FirstOrDefault(x => x.Id == 17990335);

            var from = DateTimeOffset.FromUnixTimeSeconds(1534539516).ToLocalTime();

            candles.From.Should().Be(from);
        }


        [Test]
        public void GetCandlesResult_WithSuccessFul_ResultMustReturnSuccess() {
            //arrange

            //act
            var result = JsonConvert.DeserializeObject<GetCandleItemsResultMessage>(Json);


            //assert
            result.Should().NotBeNull();
            result.Message.Should().NotBeNull();
            result.Message.Count.Should().Be(100);
        }
    }
}