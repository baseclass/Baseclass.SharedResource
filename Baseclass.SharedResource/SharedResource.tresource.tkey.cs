namespace Baseclass.SharedResource
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading;
    using System.Threading.Tasks;
    using Nito.AsyncEx;

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
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleType",
         Justification = "Reviewed.")]
    public class SharedResource<TResource, TKey> : ISharedResource<TResource, TKey>
        where TResource : IAsyncDisposable
    {
        private readonly AsyncLock asyncLock = new AsyncLock();
        private readonly Func<TKey, CancellationToken, Task<TResource>> createAsync;

        private readonly Dictionary<TKey, ResourceCount> resources = new Dictionary<TKey, ResourceCount>();

        /// <summary>
        ///     Initializes a new instance of the <see cref="SharedResource{TResource,TKey}" /> class.
        /// </summary>
        /// <param name="createAsync">
        ///     The method to create the task which creates the resource.
        /// </param>
        public SharedResource(Func<TKey, CancellationToken, Task<TResource>> createAsync)
        {
            this.createAsync = createAsync;
        }

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
        public async Task<IToken<TResource>> GetResourceAsync(TKey key, CancellationToken cancellationToken)
        {
            using (await this.asyncLock.LockAsync(cancellationToken))
            {
                if (!this.resources.ContainsKey(key))
                {
                    this.resources.Add(key, new ResourceCount(await this.createAsync(key, cancellationToken)));
                }

                this.resources[key].UsageCount++;

                return new Token(this.resources[key].Resource, key, this.ReturnResource);
            }
        }

        private async ValueTask ReturnResource(TKey key)
        {
            using (await this.asyncLock.LockAsync())
            {
                var resource = this.resources[key];
                resource.UsageCount--;
                if (resource.UsageCount == 0)
                {
                    await resource.Resource.DisposeAsync();
                    this.resources.Remove(key);
                }
            }
        }

        private class ResourceCount
        {
            public ResourceCount(TResource resource)
            {
                this.Resource = resource;
                this.UsageCount = 0;
            }

            public TResource Resource { get; }

            public int UsageCount { get; set; }
        }

        private class Token : IToken<TResource>
        {
            private readonly Func<TKey, ValueTask> disposeTask;
            private readonly TKey key;

            public Token(TResource resource, TKey key, Func<TKey, ValueTask> disposeTask)
            {
                this.Resource = resource;
                this.disposeTask = disposeTask;
                this.key = key;
            }

            #region IToken<TResource> Members

            public TResource Resource { get; }

            public ValueTask DisposeAsync()
            {
                return this.disposeTask(this.key);
            }

            #endregion
        }
    }
}