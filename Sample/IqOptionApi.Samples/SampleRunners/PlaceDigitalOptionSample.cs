using System;
using System.Threading.Tasks;
using IqOptionApi.Models;
using IqOptionApi.Models.DigitalOptions;

namespace IqOptionApi.Samples.SampleRunners
{
    public class PlaceDigitalOptionSample : SampleRunner
    {
        public override async Task RunSample()
        {
            if (await IqClientApi.ConnectAsync())
            {
                IqClientApi.WsClient.OrderChangedObservable().Subscribe(x =>
                {
                    Console.WriteLine(string.Format("OrderChanged - {0}, InstrumentId: {1}, Side: {2}, Margin(Amount): {3}",
                        x.OrderChangedEventInfo.Id,
                        x.OrderChangedEventInfo.InstrumentId,
                        x.OrderChangedEventInfo.Side,
                        x.OrderChangedEventInfo.Margin));
                });
                
                while (true)
                {
                    await Task.Delay(10000);
                    var position = await IqClientApi.WsClient.PlaceDigitalOptions(ActivePair.EURUSD,
                        OrderDirection.Call, DigitalOptionsExpiryDuration.M1, 1);

                    Console.WriteLine($"Placed position Id: {position.Id}");
                }
            }
        }
    }
}