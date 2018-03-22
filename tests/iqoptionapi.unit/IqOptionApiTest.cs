using System.Threading.Tasks;
using Autofac;
using NUnit.Framework;
using Shouldly;

namespace iqoptionapi.unit
{
    [TestFixture]
    public class IqOptionApiTest : BaseUnitTest {

        [Test]
        public async Task ConnectAsyncTask_WithValidUserId_ConnectMustBeTrue() {
            

            //arrange
            var api = new IqOptionApi(Configuration.Email, Configuration.Password);

            //act
            var result = await api.ConnectAsync();

            //assert
            result.ShouldBeTrue();
        }
    }
}

