# iqoption.net
iqoption api to connect to www.iqoption.com (unofficial)


# Milestone
- Get Candles information
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
  
  // get candles data with 100 candles size till now
  var candles = await api.GetCandlesAsync(ActivePair.EURUSD, 1, 100, DateTimeOffset.Now);
  _logger.LogInformation($"Candles received {candles.Count}");

}

```
