using System;
using System.Threading.Tasks;
using IqOptionApi.models.CFD;
using IqOptionApi.Models;

namespace IqOptionApi.ws {
    public interface IIqWsClient : IDisposable {

        IObservable<HeartBeat> Heartbeat { get; }
        IObservable<ServerTime> ServerTime { get; }
        IObservable<Profile> ProfileUpdated { get; }
        IObservable<string> ChannelMessage { get; }

        IObservable<DigitalInfoData> DigitalInfoData { get; }

        Task<bool> OpenSecuredConnectionAsync(string token);
        string SecuredToken { get; }
    }
}