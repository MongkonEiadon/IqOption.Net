using System;
using FluentAssertions;
using IqOptionApi.Models;
using IqOptionApi.Models.DigitalOptions;
using NUnit.Framework;

namespace IqOptionApi.Tests.Models
{
    [TestFixture]
    public class DigitalOptionsIdentitiferTest
    {
        [TestCase(ActivePair.EURCHF, OrderDirection.Call, DigitalExpiryDuration.M15, "doEURCHF201811101250PT15MCSPT")]
        [TestCase(ActivePair.GBPUSD, OrderDirection.Put, DigitalExpiryDuration.M5, "doGBPUSD201811101250PT5MPSPT")]
        [TestCase(ActivePair.EURUSD, OrderDirection.Call, DigitalExpiryDuration.M1, "doEURUSD201811101250PT1MCSPT")]
        public void TestCreate_InstrumentId(
            ActivePair active,
            OrderDirection direction,
            DigitalExpiryDuration duration,
            string expected)
        {
            var specificDate = DateTimeOffset.Parse("2018-11-10 12:50:50Z");

            var result = new DigitalOptionsIdentifier(active, direction, duration, specificDate);

            result.ToString()
                .Should().Be(expected);
        }
    }
}