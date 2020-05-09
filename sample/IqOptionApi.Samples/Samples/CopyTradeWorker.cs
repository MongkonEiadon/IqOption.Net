using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IqOptionApi.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace IqOptionApi.CopyTrade
{
    public class CopyTradeWorker : BackgroundService
    {
        private readonly ILogger<CopyTradeWorker> _logger;

        public CopyTradeWorker(ILogger<CopyTradeWorker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var trader = new IqOptionClient("a@b.com", "changeme");
            var follower = new IqOptionClient("b@c.com", "changeme"); 

            await Task.WhenAll(trader.ConnectAsync(), follower.ConnectAsync());

            trader.WsClient.OpenOptionObservable().Subscribe(x => {
                follower.BuyAsync(x.Active, (int) x.Amount, x.Direction, x.ExpirationTime);
            });
        }
    }
}