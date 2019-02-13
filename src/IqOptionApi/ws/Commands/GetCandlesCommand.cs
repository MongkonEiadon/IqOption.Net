using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IqOptionApi.Extensions;
using IqOptionApi.Models;
using IqOptionApi.ws.Request;

namespace IqOptionApi.ws
{

    //#{"name":"sendMessage","request_id":"140","msg":{"name":"get-order","version":"1.0","body":{"order_id":664130181}}}
    public partial class IqWsClient {

        public async Task<CandleCollections> GetCandlesAsync(ActivePair pair, TimeFrame tf, int count,
            DateTimeOffset to) {

            await SendMessageAsync(new GetCandleItemRequestMessage(pair, tf, count, to));

            return null;
        }
    }
}
