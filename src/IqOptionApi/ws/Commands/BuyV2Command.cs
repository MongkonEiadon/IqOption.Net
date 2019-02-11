using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using IqOptionApi.Extensions;
using IqOptionApi.Models;
using IqOptionApi.ws.Request;

namespace IqOptionApi.ws
{
    public partial class IqWsClient
    {
        public Task<BuyResult> BuyAsync(ActivePair pair, int size, OrderDirection direction,
            DateTimeOffset expiration = default(DateTimeOffset))
        {

            var tcs = new TaskCompletionSource<BuyResult>();
            try
            {

                var obs = this.ToObservable(x => x.BuyResult)
                    .Subscribe(x => { tcs.TrySetResult(x); });

                tcs.Task.ContinueWith(x => {
                    if (x.Result != null)
                    {
                        obs.Dispose();
                    }
                });

                if (expiration.Second % 60 != 0)
                    expiration = expiration.AddSeconds(60 - expiration.Second);

                SendMessageAsync(new BuyV2WsMessage(pair, size, direction, expiration, DateTimeOffset.Now)).ConfigureAwait(false);

            }
            catch (Exception ex)
            {
                tcs.TrySetException(ex);
            }

            return tcs.Task;
        }
    }
}
