# iqoption.net
iqoption api to connect to www.iqoption.com (unofficial)

# How to use
```csharp
var client = new IqOptionApi("emailaddress", "password");

//begin connect
if(await client.ConnectAsync()){

  //get user profile
  var profile = await client.GetProfileAsync();

}

```
