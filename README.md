# IqOption.Net Plus

### A new version of IqOption.Net
IqOption API to connect to www.iqoption.com (Unofficial), with .NET Core based
Now we can talk about the issue on gitter here

# Changelog
- Fix login
- Active Pair profit (Binary / Digital)
- Use [WebSocket.Client](https://github.com/Marfusios/websocket-client) *(Support High-Concurrent)*

# Milestone
- Performance Improve
- Edit README.md
- Donate me (;-;)

# Quickstart

```csharp
using IqOptionApi;
```

```csharp
IqOptionClient Client = new IqOptionClient("example@web.com", "P@ssw0rd");
```
Boom!

# Use-case
- Basic Authentication
```csharp
IqOptionClient Client = new IqOptionClient("example@web.com", "P@ssw0rd");
bool IsConnected = await Client.ConnectAsync();
```
- Change Balance Type *(Practice, Real, Tournament)*
```csharp
using IqOptionApi.Model;
// ....
var ProfileData = Client.WsClient.Profile;
var BalanceData = ProfileData.Balances.FirstOrDefault(x => x.Type == BalanceType.Practice);
await Client.ChangeBalanceAsync(BalanceData.Id);
```
- Initialization Data *(Binary & Turbo)*
```csharp
using IqOptionApi.Model;
// ....
InitializationData Data = await Client.WsClient.GetInitializationData();
foreach (ActiveInitializationData ActiveData in Data?.Turbo?.Actives) // or Data?.Binary?.Actives
{
    // Check whice Active Pair is active
    if (ActiveData.GroupID < 1 || ActiveData.Enabled == false) continue;
    ActivePair Pair = ActiveData.ActivePair;
    int MinimumBet = ActiveData.MinimumBet;
    int MaximumBet = ActiveData.MaximumBet;
    int Profit = ActiveData.Profit;
    // ....
}
```
- Instrument Quotes *(Digital Only)*
```csharp
using IqOptionApi.Model;
// ....
List<int> ExpirationTimes = new List<int>(){ 
  (int)DigitalOptionsExpiryDuration.M1,
  (int)DigitalOptionsExpiryDuration.M5,
  (int)DigitalOptionsExpiryDuration.M15,
};
ActivePair Pair = ActivePair.EURUSD;
foreach(int Expiration in ExpirationTimes){
  InstrumentType InstType = InstrumentType.DigitalOption;
  Client.SubscribeInstrumentQuotes(Pair, Expiration, InstType);
  Client.WsClient.InstrumentQuotesObservable().Subscribe(Quote =>
  {
      foreach (InstrumentQuotesData QuoteData in Quote.Quotes)
      {
          if (QuoteData.Symbols.Count(x => x.IndexOf("SPT") > 0) > 0 && QuoteData.Price.Ask != null)
          {
              // If profit is less than 0, it means this pair is not open. 
              double RawProfit = Convert.ToDouble(((100 - QuoteData.Price.Ask) * 100) / QuoteData.Price.Ask);
              int Profit = Convert.ToInt32(Math.Floor(RawProfit));
              int ExpirationTime = Expiration;
              // .....
          }
      }
  });
}
```
