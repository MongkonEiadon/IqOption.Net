using System;
using System.Linq;
using System.Runtime.CompilerServices;
using IqOptionApi.Converters.JsonConverters;
using IqOptionApi.Models;
using IqOptionApi.Ws.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

[assembly: InternalsVisibleTo("IqOptionApi.Unit", AllInternalsVisible = true)]

namespace IqOptionApi.Ws.Request
{
    internal class GetLeaderboardModel
    {
        [JsonProperty("country_id", Required = Required.Always)]
        public long CountryID { get; set; }

        [JsonProperty("from_position")] public int FromPosition { get; set; }

        [JsonProperty("near_traders_count")] public int NearTradersCount { get; set; }

        [JsonProperty("near_traders_country_count")] public int NearTradersCountryCount { get; set; }

        [JsonProperty("to_position")] public int ToPosition { get; set; }

        [JsonProperty("top_count")] public int TopCount { get; set; }

        [JsonProperty("top_country_count")] public int TopCountryCount { get; set; }
        [JsonProperty("top_type")] public int TopType { get; set; }

        [JsonProperty("user_country_id")]
        public long UserCountryID { get; set; }
    }

    internal sealed class GetLeaderboardWsMessage : WsSendMessageBase<GetLeaderboardModel>
    {
        public GetLeaderboardWsMessage(
            Country TargetCountryID,
            long MyCountryID,
            int From=1,
            int To=64
            )
        {
            Message = new RequestBody<GetLeaderboardModel>
            {
                RequestBodyType = RequestMessageBodyType.GetLeaderboardDeals,
                Body = new GetLeaderboardModel
                {
                    CountryID = (long)TargetCountryID,
                    NearTradersCount = 64,
                    NearTradersCountryCount = 64,

                    // default preset
                    FromPosition = From,
                    ToPosition = To,
                    TopCount = 64,
                    TopCountryCount = 64,
                    TopType = 2,
                    UserCountryID = MyCountryID
                }
            };
        }
    }
}
