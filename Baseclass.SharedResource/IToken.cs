namespace Baseclass.SharedResource
{
    using Rx.Contrib;

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
    }
}