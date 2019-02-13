using System;
using System.ComponentModel;
using System.Threading.Tasks;
using IqOptionApi.Models;
using ReactiveUI;

namespace IqOptionApi.http {
    /// <summary>
    ///     The IqOption Http Client Wrapper
    /// </summary>
    public interface IIqHttpClient : IDisposable, IReactiveObject {
        /// <summary>
        ///     The token for using secured channel
        /// </summary>
        string SecuredToken { get; }

        /// <summary>
        ///     Login Model
        /// </summary>
        LoginModel LoginModel { get; }

        /// <summary>
        ///     Profile Information
        /// </summary>
        Profile Profile { get; }

        /// <summary>
        ///     Login to the server
        /// </summary>
        /// <returns></returns>
        Task<IqHttpResult<SsidResultMessage>> LoginAsync();

        /// <summary>
        ///     Retrieved profile
        /// </summary>
        /// <returns></returns>
        Task<Profile> GetProfileAsync();
    }
}