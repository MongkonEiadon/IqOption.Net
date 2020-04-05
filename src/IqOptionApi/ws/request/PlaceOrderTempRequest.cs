using iqoptionapi.models;
using Newtonsoft.Json;

namespace IqOptionApi.ws.request
{
    public class PlaceOrderTempMessage
    {
        [JsonProperty("name")]
        public string Name => "place-order-temp";

        [JsonProperty("version")]
        public string Version => "4.0";

        [JsonProperty("body")]
        public PlaceOrder Body { get; set; }


    }

    public class PlaceOrderTempRequest : WsSendMessageBase<PlaceOrderTempMessage>
    {
        public PlaceOrderTempRequest(PlaceOrder placeOrder)
        {
            Message = new PlaceOrderTempMessage()
            {
                Body = placeOrder
            };
        }
    }
}