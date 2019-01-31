using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IqOptionApi.Models;
using IqOptionApi.ws;
using IqOptionApi.ws.request;
using NUnit.Framework;

namespace IqOptionApi.unit.ws
{
    //public class GetCandlesTest : IClassFixture<BaseUnitTest> {
    //    private readonly BaseUnitTest _baseTest;

    //    public GetCandlesTest(BaseUnitTest baseTest) {
    //        _baseTest = baseTest;
    //    }

        
    //    [Test]
    //    public async Task GetCandles() {

    //        var getCandleRequest = new GetCandleItemRequestMessage(ActivePair.EURAUD, TimeFrame.Min5, 100, DateTimeOffset.Now);
    //        var ws = new IqOptionWebSocketClient("ce9e2de1cb04bc4d06c8a5707976783f");
            
    //        await ws.SendMessageAsync(getCandleRequest);

    //        Thread.Sleep(1000);

    //        await ws.SendMessageAsync(getCandleRequest);

    //        Thread.Sleep(1000);

    //        ws.CandleCollections.ShouldNotBeNull();



    //    }
    //}
}
