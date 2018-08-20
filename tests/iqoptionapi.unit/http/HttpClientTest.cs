using System;
using System.Collections.Generic;
using System.Text;
using AutofacContrib.NSubstitute;
using iqoptionapi.http;
using NUnit.Framework;
using RestSharp;
using Xunit;

namespace iqoptionapi.unit.http
{
    public class HttpClientTest {

        public AutoSubstitute AutoSubstitute { get; private set; }
        
        public HttpClientTest() {
            AutoSubstitute = new AutoSubstitute();
        }


        [Fact]
        public void LoginAsync_WithResponse200_SsidMustReturn() {
            
            // arrange
            var client = AutoSubstitute.Resolve<IRestClient>();
            var request = AutoSubstitute.Resolve<IRestRequest>();
            

            // act
            var result = AutoSubstitute.Resolve<IqOptionHttpClient>();




        }
    }
}
