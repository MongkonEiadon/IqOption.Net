using IqOptionApi.Models;
using Serilog.Core;
using Serilog.Events;

namespace IqOptionApi.Logging
{
    internal class ProfileEnricher : ILogEventEnricher
    {
        private readonly Profile _profile;

        public ProfileEnricher(Profile profile)
        {
            _profile = profile;
        }

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            logEvent.AddPropertyIfAbsent(
                propertyFactory.CreateProperty("UserId", _profile?.UserId));
            logEvent.AddPropertyIfAbsent(
                propertyFactory.CreateProperty("Email", _profile?.Email));
            logEvent.AddPropertyIfAbsent(
                propertyFactory.CreateProperty("BalanceType", _profile?.BalanceType));
            logEvent.AddPropertyIfAbsent(
                propertyFactory.CreateProperty("Currency", _profile?.CurrencyChar));
            logEvent.AddPropertyIfAbsent(
                propertyFactory.CreateProperty("BalanceAmount", _profile?.Balance));
        }
    }
}