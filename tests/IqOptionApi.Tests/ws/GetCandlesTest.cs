using System;
using System.Threading;
using System.Threading.Tasks;
using IqOptionApi.Models;
using IqOptionApi.unit;
using IqOptionApi.Ws;
using IqOptionApi.Ws.Request;
using NUnit.Framework;

namespace IqOptionApi.Tests.ws
{
    public class GetCandlesTest : TestFor<Object>
    {

        [Test]
        public async Task GetCandles()
        {

            var getCandleRequest = new GetCandleItemRequestMessage(ActivePair.EURAUD, TimeFrame.Min5, 100, DateTimeOffset.Now);
            var ws = new IqOptionWebSocketClient("ce9e2de1cb04bc4d06c8a5707976783f");

            await ws.SendMessageAsync(getCandleRequest);

            Thread.Sleep(1000);

            await ws.SendMessageAsync(getCandleRequest);

            Thread.Sleep(1000);
        }
    }
}
