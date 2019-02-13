using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading.Tasks;
using DynamicData.Binding;
using IqOptionApi.Extensions;
using IqOptionApi.Logging;
using IqOptionApi.Models;
using IqOptionApi.Sample.Logging;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using LogProvider = IqOptionApi.Logging.LogProvider;

namespace IqOptionApi.Sample {
    public class Program {
        private static IIqOptionClient api;

        static void Main(string[] args) {

            var provider = ConfigureServices(new ServiceCollection());
            var sample = provider.GetService<Sample>();

            try {

                Console.WriteLine("IqOption.NET Sample");

                sample.RunAsync().ConfigureAwait(false);


            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
            finally {
                Console.ReadLine();
            }

        }

        private static IServiceProvider ConfigureServices(IServiceCollection services) {


            LogProvider.SetCurrentLogProvider(new ColoredConsoleLogProvider());

            services
                .AddSingleton<TradingExample>()
                .AddSingleton<Sample>();

            return services.BuildServiceProvider();
        }

    }


    public class Sample {

        public IqOptionClient api;

        public async Task RunAsync() {


            api = new IqOptionClient("mongkon.eiadon@hotmail.com", "Code11054");

            try {
                //logging in
                if (await api.ConnectAsync()) {

                    //open order EurUsd in smallest period(1min) 
                    //var exp = DateTime.Now.AddMinutes(1);
                    //await api.BuyAsync(ActivePair.EURUSD, 1, OrderDirection.Call, exp);
                    //await api.BuyAsync(ActivePair.EURUSD, 1, OrderDirection.Call, exp);
                    

                    // get candles data
                    //var candle = await api.GetCandlesAsync(ActivePair.EURUSD, TimeFrame.Min1, 100,
                    //    DateTimeOffset.Now);

                    //subscribe to pair to get real-time data for tf1min and tf5min

                    await api.SubscribeCandlesAsync(ActivePair.EURUSD, TimeFrame.Min1);
                    await api.SubscribeCandlesAsync(ActivePair.EURUSD, TimeFrame.Min5);

                    api.WsClient.WhenAnyValue(x => x.CurrentCandle)
                        .Where(x => x != null)
                        .Subscribe(candleInfo =>
                        {
                            Console.WriteLine(
                                $"Now {ActivePair.EURUSD} {candleInfo.TimeFrame} : Bid={candleInfo.Bid}\t Ask={candleInfo.Ask}\t");
                        });

                    //// after this line no-more realtime data for min5 print on console
                    //await api.UnsubscribeCandlesAsync(ActivePair.EURUSD, TimeFrame.Min5);

                    //api.WsClient.ToObservable(x => x.Profile).Subscribe(x => {
                    //    Console.WriteLine("Profileeeee");
                    //});


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