using IqOptionApi.Extensions;
using IqOptionApi.http;
using Moq;
using NUnit.Framework;
using RestSharp;
using TestAutoFixture;

namespace IqOptionApi.Tests.http
{
    [TestFixture]
    public abstract class IqHttpClientBaseTest : TestAutoFixtureFor<IqHttpClient>
    {
        protected Mock<IRestClient> MoqAuthClient { get; set; }
        protected Mock<IRestClient> MoqHttpClient { get; set; }

        [SetUp]
        public void SetUp()
        {
            MoqAuthClient = InjectMock<IRestClient>();
            MoqHttpClient = InjectMock<IRestClient>();

            Fixture.Customize<IqHttpClient>(
                cfg => cfg.FromFactory(() => new IqHttpClient("", "")
                {
                    AuthHttpClient = MoqAuthClient.Object,
                    HttpClient = MoqHttpClient.Object
                }));

            Fixture.Customize<RestResponse>(cfg =>
                cfg.With(x => x.Content, A<IqHttpResult<SsidResultMessage>>().AsJson()));
        }
    }
}