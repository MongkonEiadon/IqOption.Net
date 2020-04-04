using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;

using IqOptionApi.Models;
using IqOptionApi.utilities;

using iqoptionapi.ws.@base;

using IqOptionApi.ws.request;

// ReSharper disable once CheckNamespace
namespace IqOptionApi.ws {

    public partial class IqOptionWebSocketClient {
        
        
        private readonly Subject<CurrentCandle> _candleInfoSubject = new Subject<CurrentCandle>();
        private readonly Subject<CandleCollections> _candelsCollections = new Subject<CandleCollections>();
        
        public IObservable<CurrentCandle> CandleInfoObservable => _candleInfoSubject.AsObservable();
        public IObservable<CandleCollections> CandelsCollectionsObservable => _candelsCollections.AsObservable();

        
        public Task<CandleCollections> GetCandlesAsync(ActivePair pair, TimeFrame tf, int count, DateTimeOffset to) {
            var tcs = new TaskCompletionSource<CandleCollections>();
            try {
                var sub = CandelsCollectionsObservable.Subscribe(x => { tcs.TrySetResult(x); });
                tcs.Task.ContinueWith(t => {
                    sub.Dispose();

                    return t.Result;
                });

                SendMessageAsync(new GetCandleItemRequestMessage(pair, tf, count, to)).ConfigureAwait(false);
            }
            catch (Exception ex) {
                tcs.TrySetException(ex);
            }

            return tcs.Task;
        }

        #region [Subscribes]

        [SubscribeForTopicName(MessageType.Candles, typeof(CurrentCandle))]
        public void Subscribe(CurrentCandle type) {
            _candleInfoSubject.OnNext(type);
        }

        public void Subscribe(CandleCollections type) {
            _candelsCollections.OnNext(type);
        }
        #endregion
        

        [Predisposable]
        private void OnCandleSubscribeDisposable() {
            
            _candelsCollections.OnCompleted();
            _candleInfoSubject.OnCompleted();
        }

    }

}