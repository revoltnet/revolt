namespace Revolt.Models.Channels.Messaging
{
    public class SavedMessages : IChannel
    {
        /// <inheritdoc />
        public string Id { get; init; }
        
        /// <summary>
        /// User id.
        /// </summary>
        public string User { get; init; }

        /// <inheritdoc />
        public string Nonce { get; init; }
    }
}