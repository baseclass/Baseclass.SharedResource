namespace Baseclass.SharedResource
{
    /// <summary>
    ///     The factory responsible for creating a <see cref="ISharedResource{TResource,TKey}" /> by passing a unit test
    ///     friendly IResourceFactory.
    /// </summary>
    public interface ISharedResourceFactory
    {
        /// <summary>
        ///     Creates an instance of a <see cref="ISharedResource{TResource}" />.
        /// </summary>
        /// <param name="resourceFactory">
        ///     The resource factory to be used to create instances of the shared resource.
        ///     Is called when there is no shared resource available, the <see cref="IResource.InitializeAsync" /> method will be
        ///     called after instantiation.
        /// </param>
        /// <typeparam name="TResource">
        ///     The type of the shared resource.
        /// </typeparam>
        /// <returns>
        ///     The instance of <see cref="ISharedResource{TResource}" />.
        /// </returns>
        ISharedResource<TResource> Create<TResource>(IResourceFactory<TResource> resourceFactory)
            where TResource : IResource;

        /// <summary>
        ///     Creates an instance of a <see cref="ISharedResource{TResource}" />.
        /// </summary>
        /// <param name="resourceFactory">
        ///     The resource factory to be used to create instances of the shared resource.
        ///     Is called when there is no shared resource available, the <see cref="IResource.InitializeAsync" /> method will be
        ///     called after instantiation.
        /// </param>
        /// <param name="param">
        ///     The parameter to pass when creating the resource by using the <see cref="resourceFactory" />.
        /// </param>
        /// <typeparam name="TResource">
        ///     The type of the shared resource.
        /// </typeparam>
        /// <typeparam name="TParam">
        ///     The type of the parameter.
        /// </typeparam>
        /// <returns>
        ///     The instance of <see cref="ISharedResource{TResource}" />.
        /// </returns>
        ISharedResource<TResource> CreateWithParam<TResource, TParam>(
            IResourceFactoryWithParam<TResource, TParam> resourceFactory, TParam param)
            where TResource : IResource;

        /// <summary>
        ///     Creates an instance of a <see cref="ISharedResource{TResource,TKey}" />.
        /// </summary>
        /// <param name="resourceFactory">
        ///     The resource factory to be used to create instances of the shared resource.
        ///     Is called when there is no shared resource available, the <see cref="IResource.InitializeAsync" /> method will be
        ///     called after instantiation.
        /// </param>
        /// <typeparam name="TResource">
        ///     The type of the shared resource.
        /// </typeparam>
        /// <typeparam name="TKey">
        ///     The type of the key resource.
        /// </typeparam>
        /// <returns>
        ///     The instance of <see cref="ISharedResource{TResource,TKey}" />.
        /// </returns>
        ISharedResource<TResource, TKey> Create<TResource, TKey>(IResourceFactory<TResource, TKey> resourceFactory)
            where TResource : IResource;

        /// <summary>
        ///     Creates an instance of a <see cref="ISharedResource{TResource}" />.
        /// </summary>
        /// <param name="resourceFactory">
        ///     The resource factory to be used to create instances of the shared resource.
        ///     Is called when there is no shared resource available, the <see cref="IResource.InitializeAsync" /> method will be
        ///     called after instantiation.
        /// </param>
        /// <param name="param">
        ///     The parameter to pass when creating the resource by using the <see cref="resourceFactory" />.
        /// </param>
        /// <typeparam name="TResource">
        ///     The type of the shared resource.
        /// </typeparam>
        /// <typeparam name="TKey">
        ///     The type of the key resource.
        /// </typeparam>
        /// <typeparam name="TParam">
        ///     The type of the parameter.
        /// </typeparam>
        /// <returns>
        ///     The instance of <see cref="ISharedResource{TResource}" />.
        /// </returns>
        ISharedResource<TResource, TKey> CreateWithParam<TResource, TKey, TParam>(
            IResourceFactoryWithParam<TResource, TKey, TParam> resourceFactory, TParam param)
            where TResource : IResource;
    }
}