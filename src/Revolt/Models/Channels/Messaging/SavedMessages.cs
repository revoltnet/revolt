using System.Text.Json.Serialization;
using Revolt.Models.Channels.Information;

namespace Revolt.Models.Channels.Messaging
{
    /// <summary>
    ///     Saved messages channel.
    /// </summary>
    public class SavedMessages : IChannel
    {
        /// <inheritdoc />
        [JsonPropertyName("_id")]
        public string Id { get; init; }

        /// <summary>
        ///     User id.
        /// </summary>
        [JsonPropertyName("user")]
        public string User { get; init; }
        
        /// <inheritdoc />
        [JsonPropertyName("nonce")]
        public string Nonce { get; init; }
    }
}