using iqoptionapi.ws.@base;

namespace IqOptionApi.ws.request
{
    internal class UnsubscribeTradersMoodChanged : WsMessageBase<dynamic>
    {

        public override string Name => "unsubscribeMessage";
        public string MsgName => "traders-mood-changed";

        public UnsubscribeTradersMoodChanged(string instrument, string assetId)
        {
            base.Message = new
            {
                Name = Name,
                Msg =
                new
                {
                    Name = MsgName,
                    Params =
                    new
                    {
                        routingFilters =
                        new { instrument = instrument, asset_id = assetId }
                    }
                }
            };


        }
    }
}