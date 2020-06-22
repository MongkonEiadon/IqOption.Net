using System.Collections;
using IqOptionApi.Models;

namespace IqOptionApi.Utilities
{
    internal static class InstrumentTypeUtilities
    {
        public static string GetInstrumentTypeFullName(InstrumentType instrumentType)
        {
            var instrumentTypeName = "";
            if (instrumentType == InstrumentType.Forex)
                instrumentTypeName = "forex";
            else if (instrumentType == InstrumentType.CFD)
                instrumentTypeName = "cfd";
            else if (instrumentType == InstrumentType.Crypto)
                instrumentTypeName = "crypto";
            else if (instrumentType == InstrumentType.DigitalOption)
                instrumentTypeName = "digital-option";
            else if (instrumentType == InstrumentType.BinaryOption)
                instrumentTypeName = "binary-option";
            else if (instrumentType == InstrumentType.TurboOption)
                instrumentTypeName = "turbo-option";
            else if (instrumentType == InstrumentType.FxOption)
                instrumentTypeName = "fx-option";

            return instrumentTypeName;
        }
        
        public static InstrumentType GetInstrumentTypeFromFullName(string instrumentType)
        {
            var instrumentTypeName = default(InstrumentType);
            if (instrumentType == "forex")
                instrumentTypeName = InstrumentType.Forex;
            else if (instrumentType == "cfd")
                instrumentTypeName = InstrumentType.CFD;
            else if (instrumentType =="crypto")
                instrumentTypeName = InstrumentType.Crypto;
            else if (instrumentType == "digital-option")
                instrumentTypeName = InstrumentType.DigitalOption;
            else if (instrumentType == "binary-option")
                instrumentTypeName = InstrumentType.BinaryOption;
            else if (instrumentType == "turbo-option")
                instrumentTypeName = InstrumentType.TurboOption;
            else if (instrumentType == "fx-option")
                instrumentTypeName = InstrumentType.FxOption;

            return instrumentTypeName;
        }
    }
}