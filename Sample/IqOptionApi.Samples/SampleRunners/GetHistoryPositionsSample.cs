using IqOptionApi.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IqOptionApi.Samples.SampleRunners
{
    public class GetHistoryPositionsSample : SampleRunner
    {
        public override async Task RunSample()
        {
            if (await IqClientApi.ConnectAsync())
            {
                Console.WriteLine("Loading...");
                List<InstrumentType> instrumentTypes = new List<InstrumentType>() {
                    InstrumentType.BinaryOption,
                    InstrumentType.DigitalOption,
                    InstrumentType.TurboOption
                };
                /**
                 * 
                 *  Parameters
                 *  instrument_types => Instrument types what you want to get past positions history 
                 *  limit => amount of data to retrieve
                 *  offset => start retrieve from index (ex: 0 means from lastest order)
                 * 
                 * **/
                HistoryPositions history = await IqClientApi.GetHistoryPositions(instrumentTypes, 30, 0);
                if (history.Positions.Count <= 0) Console.WriteLine("Not found any positions");
                foreach (Positions position in history.Positions)
                {
                    long OrderID = position.ExternalID;
                    OrderResult OrderResult = position.getResult();
                    double Enrolled = position.getEnrolled();
                    ActivePair Pair = position.ActivePair;
                    // To see what iqoption callback fields, go to "Models/HistoryPositions.cs"
                    Console.WriteLine($"[{Pair}] #{OrderID} {OrderResult} , Enrolled: {Enrolled}");
                }
            }
            else
            {
                Console.WriteLine("Cannot connect to IQOption server");
            }
        }
    }
}
