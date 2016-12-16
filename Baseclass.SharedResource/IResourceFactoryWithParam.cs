namespace Baseclass.SharedResource
{
    /// <summary>
    ///     The contract for the factory which can be used to create an instance of a sharable <see cref="IResource" />.
    ///     This contract needs to be implemented by the client.
    /// </summary>
    /// <typeparam name="TResource">
    ///     The type of the sharable resource.
    /// </typeparam>
    /// <typeparam name="TParam">
    ///     The type of the parameter to pass to <see cref="Create" />.
    /// </typeparam>
    public interface IResourceFactoryWithParam<out TResource, in TParam>
        where TResource : IResource
    {
        /// <summary>
        ///     Creates an instance of the resource.
        /// </summary>
        /// <param name="param">
        ///     The parameter to pass the constructor.
        /// </param>
        /// <returns>
        ///     An instance of the resource which should be shared.
        /// </returns>
        TResource Create(TParam param);
    }

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
    /// <typeparam name="TParam">
    ///     The type of the parameter to pass to <see cref="Create" />.
    /// </typeparam>
    public interface IResourceFactoryWithParam<out TResource, in TKey, in TParam>
        where TResource : IResource
    {
        /// <summary>
        ///     Creates an instance of the resource.
        /// </summary>
        /// <param name="key">
        ///     The key which has be used to request the resource.
        /// </param>
        /// <param name="param">
        ///     The parameter to pass the constructor.
        /// </param>
        /// <returns>
        ///     An instance of the resource which should be shared.
        /// </returns>
        TResource Create(TKey key, TParam param);
    }
}