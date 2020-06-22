using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using IqOptionApi.Models;

namespace IqOptionApi.Samples.SampleRunners
{
    public class SubscribeTradersMoodSample : SampleRunner{
        public override async Task RunSample(){

            if(await IqClientApi.ConnectAsync()){

                // call the subscribe to listening when mood changed
                IqClientApi.WsClient.TradersMoodObservable().Subscribe(x => {

                    // values goes here
                    _logger.Information(
                        $"TradersMood on {x.InstrumentType} - {x.ActivePair} values Higher :{x.Higher}, Lower: {x.Lower}"
                    );

                });

                // begin subscribe 2 pairs
                IqClientApi.SubscribeTradersMoodChanged(InstrumentType.BinaryOption, ActivePair.EURUSD);
                IqClientApi.SubscribeTradersMoodChanged(InstrumentType.BinaryOption, ActivePair.GBPUSD);

                //wait for 10 secs
                await Task.Delay(10000);

                // after unsubscribe GBPUSD moods will not come anymore
                IqClientApi.UnSubscribeTradersMoodChanged(InstrumentType.BinaryOption, ActivePair.GBPUSD);

            }
        }
    }
}