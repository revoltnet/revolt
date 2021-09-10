using Revolt.Models.Users.Information;

namespace Revolt.Models.Channels.Information
{
    /// <summary>
    /// Channel interface.
    /// </summary>
    public interface IChannel
    {
        /// <summary>
        /// Channel Id.
        /// </summary>
        string Id { get; init; }
        
        /// <summary>
        /// Nonce hash.
        /// </summary>
        string Nonce { get; init; }
    }
}