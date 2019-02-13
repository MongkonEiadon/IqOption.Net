using System;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using DynamicData.Binding;
using IqOptionApi.Extensions;
using IqOptionApi.Models;
using IqOptionApi.ws.Request;
using ReactiveUI;

namespace IqOptionApi.ws
{
    public partial class IqWsClient
    {
        public Task BuyAsync(ActivePair pair, int size, OrderDirection direction,
            DateTimeOffset expiration = default(DateTimeOffset))
        {

            try {

                if (expiration.Second % 60 != 0)
                    expiration = expiration.AddSeconds(60 - expiration.Second);

                SendMessageAsync(new BuyV2WsMessage(pair, size, direction, expiration, DateTimeOffset.Now))
                    .ConfigureAwait(false);


                return Task.CompletedTask;
            }
            catch (Exception ex) {
                return Task.FromException(ex);
            }
            
        }
    }
}
