using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using IqOptionApi.http;
using IqOptionApi.http.Commands;
using Moq;
using NUnit.Framework;
using RestSharp;
using TestAutoFixture;

namespace IqOptionApi.Tests.http
{
    [TestFixture]
    public class ChangeBalanceTest: TestAutoFixtureFor<IqHttpClient>
    {
        private Mock<IRestClient> MoqAuthClient { get; set; }
        private Mock<IRestClient> MoqHttpClient { get; set; }

        [SetUp]
        public void Setup()
        {
            MoqAuthClient = InjectMock<IRestClient>();
            MoqHttpClient = InjectMock<IRestClient>();

            Fixture.Customize<IqHttpClient>(
                cfg => cfg.FromFactory(() => {
                    var cut = new IqHttpClient("", "") {
                        AuthHttpClient = MoqAuthClient.Object,
                        HttpClient = MoqHttpClient.Object
                    };

                    return cut;
                }));
        }

        [Test]
        public async Task ChangeBalance_WithSuccess() {

            // arrange
            Fixture.Customize<RestResponse>(cfg =>
                cfg.With(x => x.StatusCode, HttpStatusCode.OK)
                   .With(x => x.Content, @"{ 'isSuccessful': true, 'message': [],  'result': null }"));

            MoqHttpClient.Setup(x => x.ExecuteTaskAsync(Any<IRestRequest>())).ReturnsAsync(A<RestResponse>());

            // act
            var api = await CreateCut().ChangeBalanceAsync(A<long>());

            // assert
            api.IsSuccessful.Should().BeTrue();
        }


        [Test]
        public async Task ChangeBalance_WithNotSuccess_MessageShouldResponse()
        {
            // arrange
            Fixture.Customize<RestResponse>(cfg =>
                cfg.With(x => x.StatusCode, HttpStatusCode.OK)
                    .With(x => x.Content, @"{ 'isSuccessful': false, 'message': 'Balance not your',  'result': null }"));

            MoqHttpClient.Setup(x => x.ExecuteTaskAsync(Any<IRestRequest>())).ReturnsAsync(A<RestResponse>());

            // act
            var api = await CreateCut().ChangeBalanceAsync(A<long>());

            // assert
            api.IsSuccessful.Should().BeFalse();
            api.Message.ToString().Should().Be("Balance not your");
        }
    }
}
