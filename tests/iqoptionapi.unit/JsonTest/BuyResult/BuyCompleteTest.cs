using FluentAssertions;
using IqOptionApi.Unit.JsonTest;
using NUnit.Framework;
using System;
using IqOptionApi.ws.result;

namespace IqOptionApi.Unit.JsonTest.BuyResult
{
    [TestFixture]
    public class BuyCompleteTest : LoadJsonFileTest<BuyCompleteResultMessage> {
        public override string JsonSourceFileName => "BuyResult\\buycomplete.json";

        [Test]
        public void LoadBuyComplete_WithSuccessResult_DateTimeConverted() {

            // act
            var result = ReadFileSource();

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
            var result = ReadFileSource();

            // assert
            var buy = result.Message.Result;
            buy.UserId.Should().Be(1234);
        }

        [Test]
        public void LoadBuyComplete_ValidateDate()
        {
            // act
            var result = ReadFileSource();

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