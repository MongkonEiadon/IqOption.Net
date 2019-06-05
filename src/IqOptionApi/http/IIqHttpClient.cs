﻿using System;
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

        /// <summary>
        ///     Change balance
        /// </summary>
        /// <param name="balanceId"></param>
        /// <returns></returns>
        Task<IqHttpResult<IHttpResultMessage>> ChangeBalance(long balanceId);

        /// <summary>
        /// Get current balance mode
        /// </summary>
        /// <returns></returns>
        Task<BalanceType> GetBalanceModeAsync();
    }
}