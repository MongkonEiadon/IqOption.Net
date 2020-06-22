using System;
using System.Threading.Tasks;
using IqOptionApi.Models;
using IqOptionApi.Models.DigitalOptions;
using IqOptionApi.Ws;

namespace IqOptionApi.Samples.SampleRunners
{
    public class PlaceDigitalOptionSample //: SampleRunner
    {
        public  async Task RunSample()
        {
            var IqClientApi = new IqOptionWebSocketClient("f3f6e297e92f17161f099cef0c6fff71");
            
                IqClientApi.PositionChangedObservable().Subscribe(x =>
                {
                    Console.WriteLine(string.Format("PortfolioChange - {0}, InstrumentId: {1}, Side: {2} {3}, Margin(Amount): {4}",
                        x.Id,
                        x.InstrumentId,
                        x.PortfolioChangedEventInfo.Status ,
                        x.PortfolioChangedEventInfo.Type,
                        x.InvestAmount));
                });

                while (true)
                {
                    await Task.Delay(TimeSpan.FromSeconds(10));
                    var position = await IqClientApi.PlaceDigitalOptions(ActivePair.EURUSD,
                        OrderDirection.Call, DigitalOptionsExpiryDuration.M1, 1);

                    Console.WriteLine($"Placed position Id: {position.Id}");
                }

        }
    }
}