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
    internal class GetInitializationDataModel
    {
    }

    internal sealed class GetInitializationDataWsMessage : WsSendMessageBase<GetInitializationDataModel>
    {
        public GetInitializationDataWsMessage()
        {
            Message = new RequestBody<GetInitializationDataModel>
            {
                RequestBodyType = RequestMessageBodyType.RequestInitializationData,
                Body = new GetInitializationDataModel
                {
                }
            };
        }
    }
}
