using iqoptionapi.ws.@base;

namespace IqOptionApi.ws.request
{
    internal class SubscribeTradersMoodChanged : WsMessageBase<dynamic>
    {

        public override string Name => "subscribeMessage";
        public string MsgName => "traders-mood-changed";

        public SubscribeTradersMoodChanged(string instrument, string assetId)
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