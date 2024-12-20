﻿namespace Baseclass.SharedResource
{
    using System;

    /// <summary>
    ///     A disposable token to a shared resource.
    /// </summary>
    /// <typeparam name="TResource">
    ///     The type of the shared resource.
    /// </typeparam>
    public interface IToken<out TResource> : IAsyncDisposable
    {
        /// <summary>
        ///     Gets the shared resource.
        /// </summary>
        TResource Resource { get; }

        /// <summary>
        ///     Gets a value if a token is valid.
        ///     A token is not valid anymore if the resource was reset.
        /// </summary>
        bool IsTokenValid { get; }
    }
}