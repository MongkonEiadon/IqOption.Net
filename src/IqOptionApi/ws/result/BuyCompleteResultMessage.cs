using IqOptionApi.Models;
using IqOptionApi.ws;
using IqOptionApi.ws.@base;

namespace IqOptionApi.ws.result
{
    public class BuyCompleteResultMessage : WsMessageBase<WsMessageWithSuccessfulResult<BuyResult>> { }
}