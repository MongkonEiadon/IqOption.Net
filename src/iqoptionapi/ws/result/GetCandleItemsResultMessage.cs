using iqoptionapi.ws.@base;
using IqOptionApi.Models;

namespace IqOptionApi.ws
{


    public class GetCandleItemsResultMessage : WsMessageBase<CandleCollections>
    {

    }

    public class CurrentCandleInfoResultMessage : WsMessageBase<CurrentCandle>
    {

    }
}