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

        private readonly Subject<DigitalOptionsPlacedResult> _placeDigitalOptionResult = new Subject<DigitalOptionsPlacedResult>();
        public IObservable<DigitalOptionsPlacedResult> PlaceDigitalOptionResultObservable => _placeDigitalOptionResult.AsObservable();
        

        [SubscribeForTopicName(MessageType.PlacedDigitalOptions, typeof(DigitalOptionsPlacedResult))]
        public void Subscribe(DigitalOptionsPlacedResult type)
        {
            _placeDigitalOptionResult.OnNext(type);
        }


        #endregion
    }
}