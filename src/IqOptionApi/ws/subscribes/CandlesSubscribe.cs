using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using IqOptionApi.Models;
using IqOptionApi.utilities;
using IqOptionApi.Ws.Base;
using IqOptionApi.Ws.Request;

// ReSharper disable once CheckNamespace
namespace IqOptionApi.Ws
{
    public partial class IqOptionWebSocketClient
    {
        private readonly Subject<CandleCollections> _candelsCollections = new Subject<CandleCollections>();


        private readonly Subject<CurrentCandle> _candleInfoSubject = new Subject<CurrentCandle>();

        public IObservable<CurrentCandle> CandleInfoObservable => _candleInfoSubject.AsObservable();
        public IObservable<CandleCollections> CandelsCollectionsObservable => _candelsCollections.AsObservable();


        public Task<CandleCollections> GetCandlesAsync(ActivePair pair, TimeFrame tf, int count, DateTimeOffset to)
        {
            var tcs = new TaskCompletionSource<CandleCollections>();
            try
            {
                var sub = CandelsCollectionsObservable.Subscribe(x => { tcs.TrySetResult(x); });
                tcs.Task.ContinueWith(t =>
                {
                    sub.Dispose();

                    return t.Result;
                });

                SendMessageAsync(new GetCandleItemRequestMessage(pair, tf, count, to)).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                tcs.TrySetException(ex);
            }

            return tcs.Task;
        }


        [Predisposable]
        private void OnCandleSubscribeDisposable()
        {
            _candelsCollections.OnCompleted();
            _candleInfoSubject.OnCompleted();
        }

        #region [Subscribes]

        [SubscribeForTopicName(MessageType.Candles, typeof(CandleCollections))]
        public void Subscribe(CandleCollections type)
        {
            _candelsCollections.OnNext(type);
        }

        [SubscribeForTopicName(MessageType.Quotes, typeof(CurrentCandle))]
        public void Subscribe(CurrentCandle type)
        {
            _candleInfoSubject.OnNext(type);
        }


        #endregion
    }
}