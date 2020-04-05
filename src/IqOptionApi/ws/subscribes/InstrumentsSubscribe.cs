using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using IqOptionApi.Models;

// ReSharper disable once CheckNamespace
namespace IqOptionApi.Ws
{
    public partial class IqOptionWebSocketClient
    {
        #region [Instruments]

        private InstrumentResultSet _instrumentResultSet = new InstrumentResultSet();
        private readonly Subject<InstrumentResultSet> _instrumentResultSetSubject = new Subject<InstrumentResultSet>();

        public IObservable<InstrumentResultSet> InstrumentResultSetObservable =>
            _instrumentResultSetSubject.Publish().RefCount();


        public Task<InstrumentResultSet> SendInstrumentsRequestAsync()
        {
            var tcs = new TaskCompletionSource<InstrumentResultSet>();
            try
            {
                _logger.Verbose(nameof(SendInstrumentsRequestAsync));

                //subscribe for the lastest result
                InstrumentResultSetObservable
                    .Subscribe(x => { tcs.TrySetResult(x); });

                //clear before query new 
                _instrumentResultSet = new InstrumentResultSet();

                //execute them all
                Task.WhenAll(
                    //SendMessageAsync(GetInstrumentWsMessageBase.CreateRequest(InstrumentType.Forex)),
                    //SendMessageAsync(GetInstrumentWsMessageBase.CreateRequest(InstrumentType.CFD)),
                    //SendMessageAsync(GetInstrumentWsMessageBase.CreateRequest(InstrumentType.Crypto))
                );
            }

            catch (Exception ex)
            {
                tcs.TrySetException(ex);
            }

            return tcs.Task;
        }

        #endregion
    }
}