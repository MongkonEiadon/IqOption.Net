using System;
using System.Globalization;
using System.Runtime.Serialization;
using IqOptionApi.Extensions;
using IqOptionApi.Utilities;

namespace IqOptionApi.Models.DigitalOptions
{
    
    /// <summary>
    /// The expiration periods to place the position of Options
    /// </summary>
    public enum DigitalExpiryDuration
    {
        [EnumMember(Value = "1M")]
        M1 = 1,
        
        [EnumMember(Value = "5M")]
        M5 = 5,
        
        [EnumMember(Value = "15M")]
        M15 = 15
    }

    public class DigitalOptionsIdentifier
    {
        public string InstrumentId { get; }
        public ActivePair Pair { get; }
        public OrderDirection Direction { get; }
        public DigitalExpiryDuration Duration { get; }
        public DateTimeOffset Expiration { get; }

        public DigitalOptionsIdentifier(
            ActivePair pair, 
            OrderDirection direction,
            DigitalExpiryDuration duration,
            DateTimeOffset now)
        {
            Pair = pair;
            Direction = direction;
            Duration = duration;
            Expiration = DateTimeUtilities.GetExpirationTime(now.UtcDateTime, duration);
        }

        public DigitalOptionsIdentifier(string instrumentId)
        {
            InstrumentId = instrumentId;
        }

        public string CreateInstrumentId()
        {
            var active = Pair.GetEnumMemberValue();
            var dateFormat = Expiration.ToString("yyyyMMdd", CultureInfo.CreateSpecificCulture("en-US"));
            var timeFormat = Expiration.ToString("HHmm");
            var duration = Duration.GetEnumMemberValue();
            var action = Direction == OrderDirection.Call ? "C" : "P";
            
            return $"do{active}{dateFormat}{timeFormat}PT{duration}{action}SPT";
        }
        
        //do EURUSD 2020 05 28 18 45 PT 15M C SPT
        public override string ToString() => CreateInstrumentId();
    }
}