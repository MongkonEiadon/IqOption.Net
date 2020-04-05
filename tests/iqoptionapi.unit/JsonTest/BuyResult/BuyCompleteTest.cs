using System;
using System.IO;

using FluentAssertions;

using IqOptionApi.ws.result;
using Newtonsoft.Json;

using NUnit.Framework;

namespace IqOptionApi.unit.JsonTest
{
    public class BuyCompleteTest : TestFor<LoadJsonFileTest> {

        public string Json { get; set; }

        [SetUp]
        public void Setup() {

            var path =
                Fluent.IO.Path.Get(Path.GetFullPath(AppContext.BaseDirectory))
                    .Combine("Json", "BuyResult", "buycomplete.json");
            
            Json = CreateUnit().LoadJson(path.FullPath);
        }


        [Test]
        public void LoadBuyComplete_WithSuccessResult_DateTimeConverted() {

            // act
            var result = JsonConvert.DeserializeObject<BuyCompleteResultMessage>(Json);

            // assert
            result.Should().NotBeNull();
            result.Message.Should().NotBeNull();

            var msg = result.Message;
            msg.IsSuccessful.Should().BeTrue();
            msg.GetMessageDescription().Should().Be("Successful");

            var buy = msg.Result;

            buy.UserId.Should().Be(1234);
        }


        [Test]
        public void LoadBuyComplete_ValidateUserData() {

            // act
            var result = JsonConvert.DeserializeObject<BuyCompleteResultMessage>(Json);

            // assert
            var buy = result.Message.Result;

            buy.UserId.Should().Be(1234);
        }

        [Test]
        public void LoadBuyComplete_ValidateDate()
        {

            // act
            var result = JsonConvert.DeserializeObject<BuyCompleteResultMessage>(Json);

            // assert
            var buy = result.Message.Result;

            var exp = DateTimeOffset.FromUnixTimeSeconds(1535448900);
            buy.Exp.Should().Be(exp);

            var created = DateTimeOffset.FromUnixTimeSeconds(1535448820);
            buy.Created.Should().Be(created);

            var timeRate  = DateTimeOffset.FromUnixTimeSeconds(1535448820);
            buy.TimeRate.Should().Be(timeRate);
        }
    }
}