using System;
using System.Threading.Tasks;
using IqOptionApi.Models;

namespace IqOptionApi.Sample {
    public class SampleOpenOrder {

        public IqOptionApi api;

        public async Task RunAsync() {


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
        }
    }
}
