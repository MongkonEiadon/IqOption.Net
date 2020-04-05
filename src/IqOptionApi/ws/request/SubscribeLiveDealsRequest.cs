using iqoptionapi.ws.@base;

namespace IqOptionApi.ws.request
{
    internal class SubscribeLiveDealsRequest : WsMessageBase<dynamic>
    {

        public override string Name => "subscribeMessage";
        public string MsgName => "live-deal";

        public SubscribeLiveDealsRequest(string instrument, string assetId)
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
                        new { instrument_active_id = instrument, instrument_type = assetId }
                    }
                }
            };


        }
    }
}