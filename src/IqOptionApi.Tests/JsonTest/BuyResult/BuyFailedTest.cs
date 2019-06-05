﻿using FluentAssertions;
using IqOptionApi.ws.result;
using NUnit.Framework;

namespace IqOptionApi.Tests.JsonTest.BuyResult {
    [TestFixture]
    public class BuyFailedTest : LoadJsonFileTest<BuyCompleteResultMessage> {
        public override string JsonSourceFileName => "BuyResult\\buyFailed.json";

        [Test]
        public void LoadBuyComplete_WithSuccessResult_DateTimeConverted() {
            // act
            var result = ReadFileSource();

            // assert
            result.Should().NotBeNull();
            result.Message.Should().NotBeNull();

            var msg = result.Message;
            msg.IsSuccessful.Should().BeFalse();
            msg.GetMessageDescription().Should().Be("หมดเวลาสำหรับการซื้อออปชันแล้ว โปรดลองอีกครั้งภายหลัง");
        }
    }
}