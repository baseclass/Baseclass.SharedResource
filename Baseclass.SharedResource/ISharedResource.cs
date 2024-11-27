namespace Baseclass.SharedResource
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    ///     Helper class to share a resource which is <see cref="IAsyncDisposable" />.
    ///     <see cref="GetResourceAsync(TKey,System.Threading.CancellationToken)" />
    ///     is a thread safe way to get hold of a resource which can be used by multiple consumers at the same time.
    ///     The returned token needs to be disposed for the resource to be disposed when it's not used anymore.
    ///     The resources are cached per key.
    /// </summary>
    /// <typeparam name="TResource">
    ///     The type of resource which should be shared.
    /// </typeparam>
    /// <typeparam name="TKey">
    ///     The type of the key.
    /// </typeparam>
    public interface ISharedResource<TResource, in TKey>
        where TResource : IAsyncDisposable
    {
        /// <summary>
        ///     Gets a token and the resource, the resource is created if there is not an existing one for the
        ///     <paramref name="key" />.
        /// </summary>
        /// <param name="key">
        ///     The key for which to get a resource.
        /// </param>
        /// <param name="cancellationToken">
        ///     The token to be able to cancel to get the resource.
        /// </param>
        /// <returns>
        ///     A token containing the resource which should be disposed if the resource is not needed anymore.
        /// </returns>
        Task<IToken<TResource>> GetResourceAsync(TKey key, CancellationToken cancellationToken);

        /// <summary>
        ///     Resets this resource, next call to <see cref="GetResourceAsync"/> will create a new resource.
        ///     Disposing previously returned tokens will have no effect.
        /// </summary>
        /// <param name="disposeResource">
        ///     Should the resource be disposed?
        /// </param>
        Task ResetAsync(bool disposeResource);
    }

    /// <summary>
    ///     Helper class to share a resource which is <see cref="IAsyncDisposable" />.
    ///     <see cref="GetResourceAsync(System.Threading.CancellationToken)" />
    ///     is a thread safe way to get hold of a resource which can be used by multiple consumers at the same time.
    ///     The returned token needs to be disposed for the resource to be disposed when it's not used anymore.
    ///     The resources are cached per key.
    /// </summary>
    /// <typeparam name="TResource">
    ///     The type of resource which should be shared.
    /// </typeparam>
    public interface ISharedResource<TResource>
        where TResource : IAsyncDisposable
    {
        /// <summary>
        ///     Gets a token and the resource, the resource is created if there is not an existing one.
        /// </summary>
        /// <param name="cancellationToken">
        ///     The token to be able to cancel to get the resource.
        /// </param>
        /// <returns>
        ///     A token containing the resource which should be disposed if the resource is not needed anymore.
        /// </returns>
        Task<IToken<TResource>> GetResourceAsync(CancellationToken cancellationToken);

        /// <summary>
        ///     Resets this resource, next call to <see cref="GetResourceAsync"/> will create a new resource.
        ///     Disposing previously returned tokens will have no effect.
        /// </summary>
        /// <param name="disposeResource">
        ///     Should the resource be disposed?
        /// </param>
        Task ResetAsync(bool disposeResource);
    }
}