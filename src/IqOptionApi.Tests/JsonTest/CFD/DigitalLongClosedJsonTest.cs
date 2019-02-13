using NUnit.Framework;

namespace IqOptionApi.Tests.JsonTest.CFD {
    [TestFixture]
    public class DigitalLongClosedJsonTest : LoadJsonFileTest<WsMessageBase<DigitalInfoData>> {
        public override string JsonSourceFileName => "CFD\\cfd-long-closed.json";

        [Test]
        public void GetCandlesResult_WithFromAndTo_DateTimeMustSetCorrectly() {
            //act
            var result = ReadFileSource();

            result.Message.Should().NotBeNull();
        }
    }


    [TestFixture]
    public class DigitalLongOpenJsonTest : LoadJsonFileTest<WsMessageBase<DigitalInfoData>> {
        public override string JsonSourceFileName => "CFD\\cfd-long-open.json";

        [Test]
        public void GetCandlesResult_WithFromAndTo_DateTimeMustSetCorrectly() {
            // act
            var result = ReadFileSource();

            // assert
            result.Message.Should().NotBeNull();

            var msg = result.Message;
            msg.Type.Should().Be(DigitalDirection.Long);
            msg.Status.Should().Be(DigitalStatus.Open);
        }
    }
}