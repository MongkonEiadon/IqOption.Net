using System;
using System.Reactive.Linq;
using FluentAssertions;
using NUnit.Framework;
using TestFixture;
using ObservableExtensions = IqOptionApi.Extensions.ObservableExtensions;

namespace IqOptionApi.Unit.Extensions
{
    [TestFixture]
    public class ObservableExtensionsTest : TestAutoFixture.TestAutoFixture
    {

        [Test]  
        public void AsyncStart_TheElements_ShouldBeOne() {

            // arrange
            var delay = ObservableExtensions.AsyncStart(TimeSpan.FromSeconds(5));

            // act
            var sub = delay.Count().Wait();

            // assert
            sub.Should().Be(1);
        }
    }
}
