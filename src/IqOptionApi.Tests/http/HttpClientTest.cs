﻿using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using IqOptionApi.Exceptions;
using IqOptionApi.Extensions;
using IqOptionApi.http;
using IqOptionApi.Tests.Constants;
using Moq;
using NUnit.Framework;
using RestSharp;

namespace IqOptionApi.Tests.http {
    [TestFixture]
    public class LoginAsyncTest : IqHttpClientBaseTest
    {
        
        [Test]
        public async Task LoginAsync_VerifyAuthClient_MustBeReceived() {
            // arrange
            MoqAuthClient
                .Setup(x => x.ExecuteTaskAsync(Any<IRestRequest>()))
                .ReturnsAsync(A<RestResponse>());
            // act
            var result = await CreateCut().LoginAsync();

            // assert
            MoqAuthClient.Verify(x => x.ExecuteTaskAsync(It.Is<RestRequest>(r =>
                r.Method == Method.POST &&
                r.Parameters.Any(p => p.Name == "email") &&
                r.Parameters.Any(p => p.Name == "password"))));
        }


        [Test]
        public async Task LoginAsync_WhenStatusCodeIsNotOk_IsSuccessMustBeFalse() {
            // arrange
            Fixture.Customize<RestResponse>(cfg =>
                cfg.With(x => x.Content, A<IqHttpResult<SsidResultMessage>>().AsJson())
                    .With(x => x.StatusCode, HttpStatusCode.Unauthorized));

            MoqAuthClient
                .Setup(x => x.ExecuteTaskAsync(Any<IRestRequest>()))
                .ReturnsAsync(A<RestResponse>());

            // act
            var result = await CreateCut().LoginAsync();

            // assert
            result.IsSuccessful.Should().BeFalse();
        }

        [Test]
        public async Task LoginAsync_WhenStatusCodeIsOk_CookieWasAdded() {
            // arrange
            MoqAuthClient
                .Setup(x => x.ExecuteTaskAsync(Any<IRestRequest>()))
                .ReturnsAsync(HttpResponseConst.LoginSuccess);

            // act
            var result = await CreateCut().LoginAsync();

            // assert
            result.Data.Ssid.Should().NotBeNullOrEmpty();
        }

        [Test]
        public async Task LoginAsync_WhenStatusCodeIsOk_IsSuccessFulMustBeTrue() {
            // arrange
            Fixture.Customize<RestResponse>(cfg =>
                cfg.With(x => x.Content, A<IqHttpResult<SsidResultMessage>>().AsJson())
                    .With(x => x.StatusCode, HttpStatusCode.OK));

            MoqAuthClient
                .Setup(x => x.ExecuteTaskAsync(Any<IRestRequest>()))
                .ReturnsAsync(A<RestResponse>());

            // act
            var instance = await CreateCut().LoginAsync();

            // assert
            instance.IsSuccessful.Should().BeTrue();
        }

        [Test]
        public void LoginAsync_WithInvalidCredentials_IsSuccessMustBeFalse()
        {
            // arrange
            MoqAuthClient.Setup(x => x.ExecuteTaskAsync(Any<IRestRequest>()))
                .Returns(Task.FromResult(HttpResponseConst.InvalidCredentials));

            // act
            Action act = () => CreateCut().LoginAsync().Wait();

            // assert
            act.Should().Throw<IqOptionMessageExceptionBase>()
                .WithMessage("Invalid credentials");
        }
    }
}