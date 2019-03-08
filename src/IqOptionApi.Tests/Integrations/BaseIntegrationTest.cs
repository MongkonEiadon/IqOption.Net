using NUnit.Framework;

namespace IqOptionApi.Tests.Integrations {
    public abstract class BaseIntegrationTest : TestAutoFixture.TestAutoFixture {
        [OneTimeSetUp]
        public void OneTimeSetup() { }
    }
}