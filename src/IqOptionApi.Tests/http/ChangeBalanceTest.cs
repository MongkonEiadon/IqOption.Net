using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using IqOptionApi.Exceptions;
using IqOptionApi.http;
using IqOptionApi.http.Commands;
using IqOptionApi.Tests.Constants;
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
        public async Task ChangeBalance_WithSuccess()
        {

            MoqHttpClient.Setup(x => x.ExecuteTaskAsync(Any<IqOptionCommand>()))
                .ReturnsAsync(HttpResponseConst.ChangeBalanceSuccess);

            // act
            var api = await CreateCut().ChangeBalance(A<long>());

            // assert
            api.IsSuccessful.Should().BeTrue();
        }


        [Test]
        public void ChangeBalance_WithNotSuccess_MessageShouldResponse()
        {
            // arrange
            MoqHttpClient.Setup(x => x.ExecuteTaskAsync(Any<IqOptionCommand>()))
                .ReturnsAsync(HttpResponseConst.ChangeIncorrectBalance);

            // act
            Action action = () => CreateCut().ChangeBalance(A<long>()).Wait();

            // assert
            action.Should().Throw<IqOptionMessageExceptionBase>()
                .WithMessage("Balance not your");
        }
    }
}
