using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Dawn;
using IqOptionApi.Extensions;
using IqOptionApi.ws.Request;


namespace IqOptionApi.ws {

    public partial class IqWsClient {

        public Task<bool> OpenSecuredConnectionAsync(string token)
        {
            var tcs = new TaskCompletionSource<bool>();
            try
            {
                Guard.Argument(token, nameof(token)).NotNull().NotEmpty();

                SecuredToken = token;

                var limit = 0;
                this.ToObservable(x => x.Profile).Select(x => nameof(Profile))
                    .Merge(this.ToObservable(x => x.HeartBeat).Select(x => nameof(HeartBeat)))
                    .Subscribe(x => {
                        if (limit >= 2) tcs.TrySetResult(false);

                        if (x == nameof(Profile)) tcs.TrySetResult(true);

                        limit++;
                    });

                SendMessageAsync(new SsidWsMessageBase(SecuredToken)).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                tcs.TrySetException(ex);
            }

            finally
            {
                SecuredToken = token;
            }

            return tcs.Task;
        }
    }
}