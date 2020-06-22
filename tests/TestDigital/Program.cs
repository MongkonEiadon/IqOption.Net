using System;
using System.Threading.Tasks;
using IqOptionApi;
using IqOptionApi.Models;
using IqOptionApi.Models.DigitalOptions;

namespace TestDigital
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                
                var api = new IqOptionClient("adventuretimelaz11@thairiches.com", "0123456789");
                if (await api.ConnectAsync())
                {
                    while (true)
                    {
                        await Task.Delay(10000);
                        var result = await api
                            .PlaceDigitalOptions(ActivePair.EURUSD, OrderDirection.Call, DigitalExpiryDuration.M1, 1);

                        Console.WriteLine($"Position Id is {result.Id}");
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                Console.ReadLine();
            }
        }
    }
}