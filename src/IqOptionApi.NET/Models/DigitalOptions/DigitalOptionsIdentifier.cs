using System;
using System.Globalization;
using System.Text.RegularExpressions;
using IqOptionApi.Extensions;
using IqOptionApi.Utilities;

namespace IqOptionApi.Models.DigitalOptions
{
    public class DigitalOptionsIdentifier
    {
        public string InstrumentId { get; }
        public ActivePair Pair { get; }
        public OrderDirection Direction { get; }
        public DigitalOptionsExpiryDuration Duration { get; }
        public DateTimeOffset Expiration { get; }

        public DigitalOptionsIdentifier(
            ActivePair pair, 
            OrderDirection direction,
            DigitalOptionsExpiryDuration duration,
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

        public static DigitalOptionsIdentifier CreateFromInstrumentId(string instrumentId)
        {
            var groups = new Regex(
                    @"(?<type>do)(?<active>[a-zA-Z-_]*)(?<expiry>[[:digit:]]*)PT(?<duration>[[:digit:]]*M)(?<direction>[CP])SPT$")
                .Match(instrumentId).Groups;

            var active = groups["active"];
            var direction = groups["direction"];
            var duration = groups["duration"];
            var expiry = groups["expiry"];

            if (!active.Success)
                throw new ArgumentException($"Couldn't extract active from '{instrumentId}'", nameof(instrumentId));

            if (!groups["expiry"].Success)
                throw new ArgumentException($"Couldn't extract expiry from '{instrumentId}'", nameof(instrumentId));

            if (!duration.Success)
                throw new ArgumentException($"Couldn't extract duration from '{instrumentId}'", nameof(instrumentId));

            if (!direction.Success)
                throw new ArgumentException($"Couldn't extract direction from '{instrumentId}'", nameof(instrumentId));


            var expiryDate = DateTimeOffset.ParseExact(expiry.Value, "yyyyMMddHHmm", new CultureInfo("en-US"));

            return new DigitalOptionsIdentifier(
                (ActivePair) active.Value.ToEnum<ActivePair>(),
                (OrderDirection) direction.Value.ToEnum<OrderDirection>(),
                (DigitalOptionsExpiryDuration) duration.Value.ToEnum<DigitalOptionsExpiryDuration>(),
                expiryDate);
        }

        //do EURUSD 2020 05 28 18 45 PT 15M C SPT
        public override string ToString() => CreateInstrumentId();
    }
}