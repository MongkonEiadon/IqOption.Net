using System;
using System.Threading.Tasks;
using Dawn;
using IqOptionApi.ws.Request;

namespace IqOptionApi.ws {
    public partial class IqWsClient {
        /// <summary>
        ///     Set up the connection with secured token
        /// </summary>
        /// <param name="token"></param>
        public void OpenSecuredConnection(string token) {
            var tcs = new TaskCompletionSource<bool>();
            try {
                // verify the argument
                Guard.Argument(token, nameof(token)).NotNull().NotEmpty();

                // set the token to the api
                SecuredToken = token;

                // posting the secured to the channel
                SendMessageAsync(new SsidWsMessageBase(SecuredToken)).Wait();
            }
            catch (Exception ex) {
                tcs.TrySetException(ex);
            }

            finally {
                SecuredToken = token;
            }
        }
    }
}