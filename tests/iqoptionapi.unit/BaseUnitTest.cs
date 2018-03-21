using AutofacContrib.NSubstitute;
using NUnit.Framework;

namespace iqoptionapi.unit {
    [TestFixture]
    public class BaseUnitTest
    {
        protected AutoSubstitute AutoSubstitute { get; private set; }

        [OneTimeSetUp]
        public void SetUpFixture() {
            AutoSubstitute = new AutoSubstitute();
        }
        


    }
}