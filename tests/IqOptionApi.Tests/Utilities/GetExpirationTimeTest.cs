using System;
using FluentAssertions;
using IqOptionApi.Models.BinaryOptions;
using IqOptionApi.Models.DigitalOptions;
using IqOptionApi.Utilities;
using NUnit.Framework;

namespace IqOptionApi.Tests.Utilities
{
    public class GetExpirationTimeTest
    {
        [TestCase("2020-05-29 15:55:37Z", "2020-05-29 15:57:00Z")] //<30secs
        [TestCase("2020-05-29 15:59:12Z", "2020-05-29 16:00:00Z")] //<30secs
        [TestCase("2020-05-29 16:03:00Z", "2020-05-29 16:04:00Z")] //< 30secs
        [TestCase("2020-05-29 16:03:30Z", "2020-05-29 16:05:00Z")] //= 30secs
        [TestCase("2020-05-29 16:03:31Z", "2020-05-29 16:05:00Z")] //> 30secs
        public void GetExpirationTime_From1Mins_TimeShouldBeCorrect(string now, string expected)
        {
            // arrange
            var dtNow = DateTimeOffset.Parse(now);
            var dtExpected = DateTimeOffset.Parse(expected);
            
            // act
            var result = DateTimeUtilities.GetExpirationTime(dtNow, DigitalOptionsExpiryDuration.M1);
            
            // assert
            dtExpected.Should().Be(result);
        }
        
        
        [TestCase("2020-05-29 17:15:39Z", BinaryOptionsDuration.M1, "2020-05-29 17:17:00Z")]
        [TestCase("2020-05-29 17:15:39Z", BinaryOptionsDuration.M2, "2020-05-29 17:18:00Z")]
        [TestCase("2020-05-29 17:15:39Z", BinaryOptionsDuration.M3, "2020-05-29 17:19:00Z")]
        [TestCase("2020-05-29 17:15:39Z", BinaryOptionsDuration.M4, "2020-05-29 17:20:00Z")]
        [TestCase("2020-05-29 17:15:39Z", BinaryOptionsDuration.M5, "2020-05-29 17:21:00Z")] 
        /*
        [TestCase("2020-05-29 17:20:39Z", BinaryOptionsDuration.M15, "2020-05-29 17:30:00Z")]
        [TestCase("2020-05-29 17:20:39Z", BinaryOptionsDuration.M30, "2020-05-29 17:45:00Z")]
        [TestCase("2020-05-29 17:20:39Z", BinaryOptionsDuration.M45, "2020-05-29 18:00:00Z")]
        [TestCase("2020-05-29 17:20:39Z", BinaryOptionsDuration.M60, "2020-05-29 18:15:00Z")]
        [TestCase("2020-05-29 17:20:39Z", BinaryOptionsDuration.H1, "2020-05-29 18:15:00Z")] 
        */
        public void GetExpirationTime_ForBinaryDuration_TimeShouldBeCorrect(string now, BinaryOptionsDuration duration, string expected)
        {
            // arrange
            var dtNow = DateTimeOffset.Parse(now);
            var dtExpected = DateTimeOffset.Parse(expected);
            
            // act
            var result = DateTimeUtilities.GetExpirationTime(dtNow, duration);
            
            // assert
            dtExpected.Should().Be(result);
        }
        
        
        [TestCase("2020-05-29 17:30:09Z", DigitalOptionsExpiryDuration.M5, "2020-05-29 17:35:00Z")] 
        [TestCase("2020-05-29 17:33:31Z", DigitalOptionsExpiryDuration.M5, "2020-05-29 17:35:00Z")] 
        [TestCase("2020-05-29 17:34:29Z", DigitalOptionsExpiryDuration.M5, "2020-05-29 17:35:00Z")] 
        [TestCase("2020-05-29 17:34:31Z", DigitalOptionsExpiryDuration.M5, "2020-05-29 17:40:00Z")] 
        public void GetExpirationTime_Digital_TimeShouldBeCorrect(string now, BinaryOptionsDuration duration, string expected)
        {
            // arrange
            var dtNow = DateTimeOffset.Parse(now);
            var dtExpected = DateTimeOffset.Parse(expected);
            
            // act
            var result = DateTimeUtilities.GetExpirationTime(dtNow, duration);
            
            // assert
            dtExpected.Should().Be(result);
        }
        
        
    }
}