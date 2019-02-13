using IqOptionApi.Converters.JsonConverters;
using IqOptionApi.models.instruments;
using Newtonsoft.Json;

namespace IqOptionApi.ws.Request {
    internal class GetInstrumentsRequest {
        [JsonProperty("name")]
        public string Name => "get-instruments";

        [JsonProperty("version")]
        public string Version => "1.0";

        [JsonProperty("body")]
        public GetInstrumentsRequestBody InstrumentGetInstrumentsRequestBody { get; set; }


        internal class GetInstrumentsRequestBody {
            [JsonProperty("type")]
            [JsonConverter(typeof(InstrumentTypeJsonConverter))]
            public InstrumentType Type { get; set; }
        }
    }

    internal class GetInstrumentWsMessageBase : WsSendMessageBase<GetInstrumentsRequest> {
        public static IWsRequestMessage<GetInstrumentsRequest> CreateRequest(InstrumentType type) {
            return new GetInstrumentWsMessageBase {
                Message = new GetInstrumentsRequest {
                    InstrumentGetInstrumentsRequestBody = new GetInstrumentsRequest.GetInstrumentsRequestBody {
                        Type = type
                    }
                }
            };
        }
    }
}