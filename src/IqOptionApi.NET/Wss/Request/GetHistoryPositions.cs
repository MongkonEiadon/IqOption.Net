using IqOptionApi.Models;
using IqOptionApi.utilities;
using IqOptionApi.Utilities;
using IqOptionApi.Ws.Base;
using IqOptionApi.Ws.Request;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace IqOptionApi.Ws
{
    public partial class IqOptionWebSocketClient
    {
        private readonly Subject<WsMessageBase<HistoryPositions>> _HistoryPositionsResult = new Subject<WsMessageBase<HistoryPositions>>();
        public IObservable<WsMessageBase<HistoryPositions>> HistoryPositionsResultObservable => _HistoryPositionsResult.AsObservable();

        [SubscribeForTopicName(MessageType.HistoryPositions, typeof(WsMessageBase<HistoryPositions>), true)]
        public void Subscribe(WsMessageBase<HistoryPositions> type)
        {
            _HistoryPositionsResult.OnNext(type);
        }

        public Task<HistoryPositions> GetHistoryPositions(List<InstrumentType> instrument_types, int limit, int offset)
        {
            List<string> instruments = new List<string>();
            foreach (InstrumentType instrument in instrument_types) instruments.Add(InstrumentTypeUtilities.GetInstrumentTypeFullName(instrument));
            return SendMessageAsync(new GetHistoryPositionsRequest(instruments, limit, offset, Profile.BalanceId, Profile.UserId), HistoryPositionsResultObservable);
        }
    }

    internal class HistoryPositionsBody
    {
        [JsonProperty("instrument_types")]
        public List<string> Instruments { get; set; }
        [JsonProperty("limit")]
        public int Limit { get; set; }
        [JsonProperty("offset")]
        public int Offset { get; set; }
        [JsonProperty("user_balance_id")]
        public long UserBalanceID { get; set; }
        [JsonProperty("user_id")]
        public long UserID { get; set; }
    }

    internal sealed class GetHistoryPositionsRequest : WsSendMessageBase<HistoryPositionsBody>
    {
        internal GetHistoryPositionsRequest(List<string> instruments, int limit, int offset, long BalanceID, long UserID)
        {
            Message = new RequestBody<HistoryPositionsBody>
            {
                Version = "2.0",
                RequestBodyType = "portfolio.get-history-positions",
                Body = new HistoryPositionsBody
                {
                    Instruments = instruments,
                    Limit = limit,
                    Offset = offset,
                    UserBalanceID = BalanceID,
                    UserID = UserID
                }
            };
        }
    }
}
