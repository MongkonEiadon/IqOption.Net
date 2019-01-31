using System;
using FluentAssertions;
using IqOptionApi.Models;
using Newtonsoft.Json;
using NUnit.Framework;
using NUnit.Framework.Internal;


namespace IqOptionApi.unit.JsonTest {

    [TestFixture]
    public class ServerTimeTest : LoadJsonFileTest<ServerTime>
    {
        public override string JsonSourceFileName => "timesync.json";
        

        [Ignore("")]
        public void GetTimeSync_WithExistingValue_DateTimeOffsetConvertedCorrected()
        {

            // act
            var result = ReadFileSource();


            // assert
            var dt = DateTimeOffset.FromUnixTimeMilliseconds(1534749220468);

            result.Should().NotBeNull();
            result.Should().Be(dt);
        }
    }
}