using System;
using System.Collections.Generic;
using System.Linq;

namespace IqOptionApi.Models
{
    public partial class InstrumentResultSet : Dictionary<string, iqoptionapi.ws.model.Instrument[]>
    {
        public InstrumentResultSet() : base()
        {
            this[EnumInstrumentType.CFD.ToString().ToLowerInvariant()] = new iqoptionapi.ws.model.Instrument[] { };
            this[EnumInstrumentType.Crypto.ToString().ToLowerInvariant()] = new iqoptionapi.ws.model.Instrument[] { };
            this[EnumInstrumentType.Forex.ToString().ToLowerInvariant()] = new iqoptionapi.ws.model.Instrument[] { };
        }


        public iqoptionapi.ws.model.Instrument GetByActivityType(ActivePair pair)
        {
            var result = this
                .SelectMany(x => x.Value)
                .FirstOrDefault(x => x.Name == Enum.GetName(typeof(ActivePair), pair));

            return result;
        }
    }
}