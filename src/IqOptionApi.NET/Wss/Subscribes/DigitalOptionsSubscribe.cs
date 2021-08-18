using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using IqOptionApi.Models;
using IqOptionApi.Models.DigitalOptions;
using IqOptionApi.utilities;
using IqOptionApi.Ws.Base;

namespace IqOptionApi.Ws
{

    public partial class IqOptionWebSocketClient
    {
        #region [PlaceDigitalOptions]

        private readonly Subject<WsMessageBase<DigitalOptionsPlacedResult>> _placeDigitalOptionResult = new Subject<WsMessageBase<DigitalOptionsPlacedResult>>();
        public IObservable<WsMessageBase<DigitalOptionsPlacedResult>> PlaceDigitalOptionResultObservable => _placeDigitalOptionResult.AsObservable();
        

        [SubscribeForTopicName(MessageType.PlacedDigitalOptions, typeof(WsMessageBase<DigitalOptionsPlacedResult>), true)]
        public void Subscribe(WsMessageBase<DigitalOptionsPlacedResult> type)
        {
            _placeDigitalOptionResult.OnNext(type);
        }

        #endregion
    }
}