namespace Baseclass.SharedResource
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

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
    public class SharedResource<TResource> : SharedResource<TResource, object>, ISharedResource<TResource>
        where TResource : IAsyncDisposable
    {
        private readonly object defaultKey = new object();

        /// <summary>
        ///     Initializes a new instance of the <see cref="SharedResource{TResource}" /> class.
        /// </summary>
        /// <param name="createAsync">
        ///     The function which should be used to create the instance of <see cref="TResource" />.
        /// </param>
        public SharedResource(Func<CancellationToken, Task<TResource>> createAsync) : base((k, ct) => createAsync(ct))
        {
        }

        /// <summary>
        ///     Gets a token and the resource, the resource is created if there is not an existing one.
        /// </summary>
        /// <param name="cancellationToken">
        ///     The token to be able to cancel to get the resource.
        /// </param>
        /// <returns>
        ///     A token containing the resource which should be disposed if the resource is not needed anymore.
        /// </returns>
        public Task<IToken<TResource>> GetResourceAsync(CancellationToken cancellationToken)
        {
            return this.GetResourceAsync(this.defaultKey, cancellationToken);
        }
    }
}