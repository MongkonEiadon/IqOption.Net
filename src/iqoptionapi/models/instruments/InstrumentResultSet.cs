using System;
using System.Collections.Generic;
using System.Linq;
using IqOptionApi.models.Instruments;
using IqOptionApi.Models;

namespace IqOptionApi.models.instruments {
    public partial class InstrumentResultSet : Dictionary<InstrumentType, Instrument[]> {
        public InstrumentResultSet() : base() {
            this[InstrumentType.CFD] = new Instrument[] { };
            this[InstrumentType.Crypto] = new Instrument[] { };
            this[InstrumentType.Forex] = new Instrument[] { };
        }


        public Instrument GetByActivityType(ActivePair pair) {
            var result = this
                .SelectMany(x => x.Value)
                .FirstOrDefault(x => x.Name == Enum.GetName(typeof(ActivePair), pair));

            return result;
        }
    }
}