using System.Collections.Generic;
using iqoptionapi.models;
using iqoptionapi.ws.@base;

namespace IqOptionApi.ws
{
    public class PositionClosedResultMessage : WsMessageBase<PositionClosed>
    {

    }

    public class PositionBatchClosedResultMessage : WsMessageBase<List<PositionClosed>>
    {

    }
}