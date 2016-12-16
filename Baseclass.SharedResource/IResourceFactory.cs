namespace Baseclass.SharedResource
{
    /// <summary>
    ///     The contract for the factory which can be used to create an instance of a sharable <see cref="IResource" />.
    ///     This contract needs to be implemented by the client.
    /// </summary>
    /// <typeparam name="TResource">
    ///     The type of the sharable resource.
    /// </typeparam>
    /// <typeparam name="TKey">
    ///     The type of the key of the sharable resource.
    /// </typeparam>
    public interface IResourceFactory<out TResource, in TKey>
        where TResource : IResource
    {
        /// <summary>
        ///     Creates an instance of the resource.
        /// </summary>
        /// <param name="key">
        ///     The key which has be used to request the resource.
        /// </param>
        /// <returns>
        ///     An instance of the resource which should be shared.
        /// </returns>
        TResource Create(TKey key);
    }

    /// <summary>
    ///     The contract for the factory which can be used to create an instance of a sharable <see cref="IResource" />.
    ///     This contract needs to be implemented by the client.
    /// </summary>
    /// <typeparam name="TResource">
    ///     The type of the sharable resource.
    /// </typeparam>
    public interface IResourceFactory<out TResource>
        where TResource : IResource
    {
        /// <summary>
        ///     Creates an instance of the resource.
        /// </summary>
        /// <returns>
        ///     An instance of the resource which should be shared.
        /// </returns>
        TResource Create();
    }
}