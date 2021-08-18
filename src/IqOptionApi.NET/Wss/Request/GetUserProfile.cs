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
    internal class GetUserProfileModel
    {
        [JsonProperty("user_id", Required = Required.Always)]
        public long UserID { get; set; }
    }

    internal sealed class GetUserProfileWsMessage : WsSendMessageBase<GetUserProfileModel>
    {
        public GetUserProfileWsMessage(long UserID)
        {
            Message = new RequestBody<GetUserProfileModel>
            {
                RequestBodyType = RequestMessageBodyType.RequestUserProfile,
                Body = new GetUserProfileModel
                {
                    UserID = UserID,
                }
            };
        }
    }
}
