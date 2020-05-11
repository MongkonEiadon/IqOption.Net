using System;
using System.Collections.Generic;
using System.Linq;

namespace IqOptionApi.Models
{
    public class InstrumentResultSet : Dictionary<InstrumentType, Instrument[]>
    {
        public InstrumentResultSet()
        {
            this[InstrumentType.CFD] = new Instrument[] { };
            this[InstrumentType.Crypto] = new Instrument[] { };
            this[InstrumentType.Forex] = new Instrument[] { };
        }


        public Instrument GetByActivityType(ActivePair pair)
        {
            var result = this
                .SelectMany(x => x.Value)
                .FirstOrDefault(x => x.Name == Enum.GetName(typeof(ActivePair), pair));

            return result;
        }
    }
}