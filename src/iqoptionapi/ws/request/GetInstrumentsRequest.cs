using iqoptionapi.converters.JsonConverters;
using iqoptionapi.models;
using Newtonsoft.Json;

namespace iqoptionapi.ws.request {
    internal class GetInstrumentsRequest {
        [JsonProperty("name")]
        public string Name => "get-instruments";

        [JsonProperty("version")]
        public string Version => "1.0";

        [JsonProperty("body")]
        public Body InstrumentBody { get; set; }


        internal class Body {
            [JsonProperty("type")]
            [JsonConverter(typeof(InstrumentTypeJsonConverter))]
            public InstrumentType Type { get; set; }
        }
    }

    internal class GetInstrumentWsRequestMessageBase : WsRequestSendMessageBase<GetInstrumentsRequest> {
        public static IWsRequestMessage<GetInstrumentsRequest> CreateRequest(InstrumentType type) =>
            new GetInstrumentWsRequestMessageBase() {
                Message = new GetInstrumentsRequest() {
                    InstrumentBody = new GetInstrumentsRequest.Body() {
                        Type = type
                    }
                }
            };
    }
}