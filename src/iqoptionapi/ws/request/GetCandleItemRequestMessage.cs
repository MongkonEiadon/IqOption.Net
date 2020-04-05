using System;
using IqOptionApi.Models;
using IqOptionApi.Ws.Base;

namespace IqOptionApi.Ws.Request
{
    public class GetCandleItemRequestMessage : WsMessageBase<GetCandlesRequestModel>
    {
        public GetCandleItemRequestMessage(ActivePair pair, TimeFrame tf, int count, DateTimeOffset to)
        {
            base.Message = new GetCandlesRequestModel
            {
                RequestBody = new GetCandlesRequestModel.GetCandlesRequestBody
                {
                    ActivePair = pair,
                    TimeFrame = tf,
                    Count = count,
                    To = to.ToUniversalTime() // servertime should set here
                }
            };
        }

        public override string Name => MessageType.SendMessage;
    }
}