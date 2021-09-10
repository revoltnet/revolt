using Revolt.Models.Channels.Information;

namespace Revolt.Models.Channels.Messaging
{
    /// <summary>
    ///     Saved messages channel.
    /// </summary>
    public class SavedMessages : IChannel
    {
        /// <summary>
        ///     User id.
        /// </summary>
        public string User { get; init; }

        /// <inheritdoc />
        public string Id { get; init; }

        /// <inheritdoc />
        public string Nonce { get; init; }
    }
}