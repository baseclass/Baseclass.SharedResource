namespace Baseclass.SharedResource
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    ///     Contract for a resource which can be shared.
    ///     This contract needs to be implemented by the client.
    /// </summary>
    public interface IResource : IAsyncDisposable
    {
        /// <summary>
        ///     Initialize the resource.
        /// </summary>
        /// <param name="ct">
        ///     The cancellation token.
        /// </param>
        /// <returns>
        ///     The initialization task.
        /// </returns>
        Task InitializeAsync(CancellationToken ct);
    }
}