# IqOption.Net
IqOption Api to connect to www.iqoption.com (unofficial), with .netcore based for another framework you can suggest,

|Branch|Build|Nuget|
|:--:|:--:|:--:|
**master**|[![Build status](https://ci.appveyor.com/api/projects/status/sueidsbt13avwvoc/branch/master?svg=true)](https://ci.appveyor.com/project/MongkonEiadon/iqoption-net/branch/master) |[![NuGet Badge](https://buildstats.info/nuget/iqoptionapi)](https://www.nuget.org/packages/iqoptionapi/) | 
**developer(Pre-release)**|[![Build Status](https://dev.azure.com/mongkoneiadon/IqOption.net/_apis/build/status/CI%20for%20GitHub?branchName=develop)](https://dev.azure.com/mongkoneiadon/IqOption.net/_build/latest?definitionId=8&branchName=develop) | [![NuGet Badge](https://buildstats.info/nuget/iqoptionapi?includePreReleases=true)](https://www.nuget.org/packages/iqoptionapi/2.0.0-alpha1)|


# Package Installation
``` javascript
PM> Install-Package iqoptionapi -Version 2.0.2

```

# How it work
This api using websocket to communicate realtime-data to IqOption server through secured websocket channel, so the realtime meta data that come on this channel will be handles by .net reactive programming called "Rx.NET", cause of a haundred of data type stream on only one channle so we need to selected subscribe on specific topic.

# Milestone
- BuyBack Position
- Subscribe to the channel
- support open Long/Short for CFD contract (Digital Options)

# How to use
```csharp
var client = new IqOptionApi("emailaddress", "password");

//begin connect
if(await client.ConnectAsync()){

  //get user profile
  var profile = await client.GetProfileAsync();
  
  // open order EurUsd in smallest period (1min) 
  var exp = DateTime.Now.AddMinutes(1);
  var buyResult = await api.BuyAsync(ActivePair.EURUSD, 1, OrderDirection.Call, exp);
  
  // get candles data
  var candles = await api.GetCandlesAsync(ActivePair.EURUSD, TimeFrame.Min1, 100, DateTimeOffset.Now);
  _logger.LogInformation($"CandleCollections received {candles.Count}");


  // subscribe to pair to get real-time data for tf1min and tf5min
  var streamMin1 = await api.SubscribeRealtimeDataAsync(ActivePair.EURUSD, TimeFrame.Min1);
  var streamMin5 = await api.SubscribeRealtimeDataAsync(ActivePair.EURUSD, TimeFrame.Min5);

  streamMin5.Merge(streamMin1)
      .Subscribe(candleInfo => {
          _logger.LogInformation($"Now {ActivePair.EURUSD} {candleInfo.TimeFrame} : Bid={candleInfo.Bid}\t Ask={candleInfo.Ask}\t");
  });

  // after this line no-more realtime data for min5 print on console
  await api.UnSubscribeRealtimeData(ActivePair.EURUSD, TimeFrame.Min5);

}

```


## Example Use Cases
This is example use cases that this api could solve your problems

### Trading follower
```csharp
public async Task TradingFollower_ExampleAsync() {

    var trader = new IqOptionApi("trader@gmail.com", "passcode");
    var follower = new IqOptionApi("follower@gmail.com", "passcode");

    await Task.WhenAll(trader.ConnectAsync(), follower.ConnectAsync());

    trader.InfoDatasObservable.Select(x => x[0]).Where(x => x.Win == "equal").Subscribe(x => {
        follower.BuyAsync(x.ActiveId, (int) x.Sum, x.Direction, x.Expired);
    });
    
}
```

## Version 2.0.0 
Now you can open order like this
```csharp
var api = new IqOptionApi("email@email.com", "passcode");

try {
    //logging in
    if (await api.ConnectAsync()) {
        //open order EurUsd in smallest period(1min) 
        var exp = DateTime.Now.AddMinutes(1);
        await api.BuyAsync(ActivePair.EURUSD, 1, OrderDirection.Call, exp);
    }
}
catch (Exception ex) {
    Console.WriteLine(ex.Message);
}
finally {
    Console.ReadLine();
}

```

### Trading follower
now using ReactiveUI way for subscribe the changing of model following this

```csharp
using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using IqOptionApi.Models;
using ReactiveUI;

namespace IqOptionApi.Sample {
    public class TradingExample {
        public async Task RunAsync() {
            var trader = new IqOptionApi("trader@email.com", "passcode");
            var follower = new IqOptionApi("follower@email.com", "passcode");

            await Task.WhenAll(trader.ConnectAsync(), follower.ConnectAsync());

            trader.WsClient.WhenAnyValue(x => x.InfoData)
                .Where(x => x != null && x.Win == WinType.Equal)
                .Subscribe(x => { follower.BuyAsync(x.ActiveId, (int) x.Sum, x.Direction, x.Expired); });


            //var exp = DateTime.Now.AddMinutes(1);
            var exp = DateTime.Now.AddMinutes(1);
            await trader.BuyAsync(ActivePair.EURUSD, 1, OrderDirection.Call, exp);
        }
    }
}

```
# Support Me
If you've got value from any of the content which I have created, but pull requests are not your thing, then I would also very much appreciate your support by buying me a coffee.

<a href="https://www.buymeacoffee.com/6VF3XHb" target="_blank"><img src="https://www.buymeacoffee.com/assets/img/custom_images/black_img.png" alt="Buy Me A Coffee" style="height: auto !important;width: auto !important;" ></a>
