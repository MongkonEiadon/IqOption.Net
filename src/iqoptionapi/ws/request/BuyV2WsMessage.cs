using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

using iqoptionapi.ws.@base;
using IqOptionApi.Converters.JsonConverters;
using IqOptionApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

[assembly: InternalsVisibleTo("IqOptionApi.unit", AllInternalsVisible = true)]

namespace IqOptionApi.ws.request {
    
    internal class BuyV2RequestModel {
        [JsonProperty("price", Required = Required.Always)]
        public long Price { get; set; }

        [JsonProperty("active_id", Required = Required.Always)]
        public ActivePair ActivePair { get; set; }


        [JsonProperty("expired", Required = Required.Always)]
        [JsonConverter(typeof(UnixSecondsDateTimeJsonConverter))]
        public DateTimeOffset Expiration { get; set; }


        [JsonProperty("direction", Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        public OrderDirection Direction { get; set; }
        
        [JsonProperty("user_balance_id")]
        public long UserBalanceId { get; set; }
        
        [JsonProperty("option_type_id")]
        public OptionType OptionType { get; set; }

        [JsonProperty("refund_value")]
        public int RefundValue { get; set; }
        
        [JsonProperty("profit_percent")]
        public long ProfitPercent { get; set; }
        
        [JsonProperty("value")]
        public int Value { get; set; }



    }

    public class ExpirationModel {

        public OptionType Type { get; }
        
        public DateTimeOffset Expiration { get; }


        public ExpirationModel(DateTimeOffset now, int duration) {
            
        }


        public ExpirationModel[] AvailableExpirations() {
            throw new NotImplementedException();
        }

        
        public static DateTimeOffset GetExpirationTime(DateTimeOffset dt) {

            var now = dt.ToUnixTimeSeconds();
            var exp_date = dt.AddSeconds(-dt.Second).AddMilliseconds(-dt.Millisecond);

            if (exp_date.AddMinutes(1).ToUnixTimeSeconds() - dt.ToUnixTimeSeconds() > 30)
                exp_date = exp_date.AddMinutes(1);
            else
                exp_date = exp_date.AddMinutes(2);


            // timeslot 1-5 mins
            var exp_range = Enumerable.Range(0, 5)
                .Select(x => exp_date.AddMinutes(x))
                .Select(x => x.ToUnixTimeSeconds())
                .ToList();
            
            var idx = 50;
            var index = 0;
            
            
            while (index < idx) {

                if (exp_date.Minute % 15 == 0
                    && ((exp_date.ToUnixTimeSeconds() - now) > 60 * 5)) {
                    exp_range.Add(exp_date.ToUnixTimeSeconds());
                    index += 1;
                }
 
                exp_date = exp_date.AddMinutes(1);
            }


            var remaining = exp_range
                .Select(x => Math.Abs(x - now));


            return DateTimeOffset.Now;
        }

    }


    internal class BuyV2WsMessage : WsSendMessageBase<BuyV2RequestModel> {
        
    
        /*
         {
                "name": "sendMessage",
                "msg": {
                    "name": "binary-options.open-option",
                    "version": "1.0",
                    "body": {
                        "user_balance_id": 43997693,
                        "active_id": 1,
                        "option_type_id": 3,
                        "direction": "call",
                        "expired": 1577780640,
                        "refund_value": 0,
                        "price": 1,
                        "value": 0,
                        "profit_percent": 0
                    }
                },
                "request_id": ""
            }
            
            {
                "name": "sendMessage",
                "microserviceName": null,
                "msg": {
                    "name": "binary-options.open-option",
                    "body": {
                        "price": 1,
                        "active_id": 77,
                        "exp": 1577886900,
                        "direction": "call",
                        "user_balance_id": 43997693,
                        "option_type_id": 3,
                        "refund_value": 0,
                        "profit_percent": 0,
                        "value": 0
                    },
                    "version": "1.0"
                },
                "version": "1.0"
            }
         */
        public BuyV2WsMessage(
            long balanceId,
            ActivePair pair,
            OptionType optionType,
            OrderDirection direction,
            DateTimeOffset expiration,
            int price) {

            Message = new RequestBody<BuyV2RequestModel> {
                RequestBodyType = RequestMessageBodyType.OpenOptions,
                Body = new BuyV2RequestModel {
                    UserBalanceId = balanceId,
                    ActivePair = pair,
                    OptionType = optionType,
                    Direction = direction,
                    Expiration = expiration,
                    Price = price,
                    
                    // default preset
                    Value = 0,
                    ProfitPercent = 0,
                    RefundValue = 0
                }
            };
        }
    }
}