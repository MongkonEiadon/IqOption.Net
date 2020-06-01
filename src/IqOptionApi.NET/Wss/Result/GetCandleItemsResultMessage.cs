using IqOptionApi.Models;
using IqOptionApi.Ws.Base;
using Newtonsoft.Json;

namespace IqOptionApi.Ws
{
    public class GetCandleItemsResultMessage : WsMessageBase<CandleCollections>
    {
    }

    public class CurrentCandleInfoResultMessage : WsMessageBase<CurrentCandle>
    {
    }
}