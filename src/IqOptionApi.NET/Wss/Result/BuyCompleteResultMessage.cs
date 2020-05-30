using IqOptionApi.Models;
using IqOptionApi.Models.BinaryOptions;
using IqOptionApi.Ws.Base;

namespace IqOptionApi.Ws.result
{
    internal class BuyCompleteResultMessage : WsMessageBase<WsMessageWithSuccessfulResult<BinaryOptionsResult>>
    {
    }
}