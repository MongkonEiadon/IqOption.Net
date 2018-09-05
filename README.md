# iqoption.net
iqoption api to connect to www.iqoption.com (unofficial), with .netcore based for another framework you can suggest,

[![Build status](https://ci.appveyor.com/api/projects/status/sueidsbt13avwvoc/branch/master?svg=true)](https://ci.appveyor.com/project/MongkonEiadon/iqoption-net/branch/master)
[![NuGet](https://img.shields.io/badge/nuget-1.1.0.4-blue.svg)](https://www.nuget.org/packages/iqoptionapi/)

# Package Installation
``` javascript
PM> Install-Package iqoptionapi

```
# How it work
This api using websocket to communicate realtime-data to Iqoption server through secured websocket channel, so the realtime meta data that come on this channel will be handles by .net reactive programming called "Rx.NET", cause of a haundred of data type stream on only one channle so we need to selected subscribe on specific topic.

# Milestone
- BuyBack Position

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


