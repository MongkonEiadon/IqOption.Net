using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using IqOptionApi.http;
using Moq;
using NUnit.Framework;
using RestSharp;
using TestAutoFixture;

namespace IqOptionApi.Tests.http
{
    [TestFixture]
    public class IqOptionApiTest : TestAutoFixtureFor<IqHttpClient>
    {
        

        [SetUp]
        public void SetUp() {

            Fixture.Customize<IqHttpClient>(cfg => cfg.WithAutoProperties());
        }


        [Test]
        public async Task Test() {
            var response = A<RestResponse>();
            response.StatusCode = HttpStatusCode.BadRequest;
            var httpClient = InjectMock<IRestClient>();
            httpClient
                .Setup(x => x.ExecuteTaskAsync(Any<IRestRequest>()))
                .ReturnsAsync(response);



            // act
            var instance = await CreateTestClass().LoginAsync();


        }

        private IqHttpClient CreateTestClass() {

            var _instance = CreateSut();
            _instance.HttpClient = InjectMock<IRestClient>().Object;
            _instance.AuthHttpClient = InjectMock<IRestClient>().Object;

            return _instance;
        }
    }
}
