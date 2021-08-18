using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using IqOptionApi.Models;
using IqOptionApi.utilities;
using IqOptionApi.Ws.Base;
using IqOptionApi.Ws.Request;
using IqOptionApi.Ws.Request.Portfolio;

namespace IqOptionApi.Ws
{
    public partial class IqOptionWebSocketClient
    {
        private readonly Subject<PositionChanged> _positionChangedSubject = new Subject<PositionChanged>();
        public IObservable<PositionChanged> PositionChangedObservable() => _positionChangedSubject.DistinctUntilChanged().AsObservable();

        [SubscribeForTopicName(MessageType.SubscribePortfolioChanged, typeof(PositionChanged))]
        public void Subscribe(PositionChanged value)
        {
            _positionChangedSubject.OnNext(value);
        }

        /// <summary>
        /// To subscribe the order changed support for "Forex", "Digital-Option"
        /// </summary>
        /// <returns></returns>
        public async Task SubscribePositionChanged(InstrumentType instrumentType)
        {
            if (Profile == null)
            {
                await SendMessageAsync(new SsidWsMessageBase(SecureToken)).ConfigureAwait(false);
                await Task.Delay(500);
            }
            if (Profile == null) return;
            await SendMessageAsync(new SubscribePortfolioPositionChangedRequest(Profile.UserId, Profile.BalanceId, instrumentType), "s_");
        }

        public void UnSubscribePositionChanged(InstrumentType instrumentType)
        {
            SendMessageAsync(new UnSubscribePositionChangedRequest(Profile.UserId, Profile.BalanceId, instrumentType), "s_").ConfigureAwait(false);
        }
    }
}