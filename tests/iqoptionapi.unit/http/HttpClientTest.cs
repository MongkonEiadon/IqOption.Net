using System;
using System.Collections.Generic;
using System.Text;
using AutofacContrib.NSubstitute;
using IqOptionApi.http;
using NUnit.Framework;
using RestSharp;
using Xunit;
using Xunit.Sdk;

namespace IqOptionApi.unit.http
{
   
    public class HttpClientTest {

        public AutoSubstitute AutoSubstitute { get; private set; }
        
        public HttpClientTest() {
            AutoSubstitute = new AutoSubstitute();
        }
        
        //[TestSkipped("Real connected")])
        //public void LoginAsync_WithResponse200_SsidMustReturn() {
            
        //    // arrange
        //    var client = AutoSubstitute.Resolve<IRestClient>();
        //    var request = AutoSubstitute.Resolve<IRestRequest>();
            

        //    // act
        //    var result = AutoSubstitute.Resolve<IqOptionHttpClient>();




        //}
    }
}
