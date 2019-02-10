using System;
using System.Threading.Tasks;
using IqOptionApi.Models;

namespace IqOptionApi.http {

    /// <summary>
    /// The IqOption Http Client Wrapper
    /// </summary>
    public interface IIqHttpClient : IDisposable {

        /// <summary>
        /// The token for using secured channel
        /// </summary>
        string SecuredToken { get; }

        /// <summary>
        /// Login Model
        /// </summary>
        LoginModel LoginModel { get; }

        /// <summary>
        /// Login to the server
        /// </summary>
        /// <returns></returns>
        Task<IqHttpResult<SsidResultMessage>> LoginAsync();

        /// <summary>
        /// Retrieved profile
        /// </summary>
        /// <returns></returns>
        Task<Profile> GetProfileAsync();

        /// <summary>
        /// Stream for profile updated event
        /// </summary>
        /// <returns></returns>
        IObservable<Profile> ProfileUpdated { get; }
    }
}