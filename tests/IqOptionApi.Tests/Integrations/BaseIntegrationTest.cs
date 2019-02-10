using System;
using System.Collections.Generic;
using System.Text;
using IqOptionApi.http;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace IqOptionApi.Tests.Integrations
{
    public abstract class BaseIntegrationTest : TestAutoFixture.TestAutoFixture
    {
        [OneTimeSetUp]
        public void OneTimeSetup() {
        }
    }
}
