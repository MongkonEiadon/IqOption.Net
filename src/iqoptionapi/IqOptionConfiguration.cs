using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("IqOptionApi.Tests")]

namespace IqOptionApi {
    public class IqOptionConfiguration {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
    }

    public interface IClient<TClientType> {
        TClientType Client { get; }
    }

    public interface IStreamMessageFor<out TType> {
        IObservable<TType> StreamMessageObservable();
    }
}