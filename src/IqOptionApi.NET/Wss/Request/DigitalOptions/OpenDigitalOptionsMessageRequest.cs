using IqOptionApi.Models.DigitalOptions;
using IqOptionApi.Ws.Base;
using IqOptionApi.Ws.Request;
using Newtonsoft.Json;

namespace IqOptionApi.Wss.Request.DigitalOptions
{
    internal class OpenDigitalOptionsBody
    {
        [JsonProperty("user_balance_id")]
        public int UserBalanceId { get; set; }
        /*
        [JsonProperty("instrument_underlying")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ActivePair Instrument { get; set; }
        */
        [JsonProperty("amount")]
        public string  Amount { get; set; }
        
        [JsonProperty("instrument_id")]
        public string InstrumentId { get; set; }
    }
    
    internal sealed class PlaceDigitalOptionsMessageRequest : WsSendMessageBase<OpenDigitalOptionsBody>
    {
        internal PlaceDigitalOptionsMessageRequest(DigitalOptionsIdentifier positionIdentifier, int userbalanceId,
            double amount)
            : this(positionIdentifier.CreateInstrumentId(), userbalanceId, amount)
        {
            
        }

        internal PlaceDigitalOptionsMessageRequest(string positionIdentifier, int userbalanceId, double amount)
        {
            Message = new RequestBody<OpenDigitalOptionsBody>
            {
                Version = "1.0",
                RequestBodyType = "digital-options.place-digital-option",
                Body = new OpenDigitalOptionsBody
                {
                    InstrumentId = positionIdentifier,
                    UserBalanceId = userbalanceId,
                    Amount = amount.ToString()
                }
            };
        }
    }
}