#nullable enable
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Revolt.Models.Channels.Information;

namespace Revolt.Models.Users.DirectMessaging
{
  
    /// <summary>
    /// Direct message.
    /// </summary>
    public class DirectMessage : IChannel
    {
        /// <inheritdoc />
        [JsonPropertyName("_id")]
        public string Id { get; init; } = null!;
        
        /// <summary>
        /// Whether this DM is active.
        /// </summary>
        [JsonPropertyName("active")]
        public bool Active { get; init; }

        /// <summary>
        /// List of user IDs who are participating in this DM
        /// </summary>
        [JsonPropertyName("recipients")]
        public List<string> Recipients { get; init; } = null!;

        /// <summary>
        /// Last message sent in channel.
        /// </summary>
        [JsonPropertyName("last_message")]
        public LastMessage LastMessage { get; init; } = null!;

        /// <inheritdoc />
        [JsonPropertyName("nonce")]
        public string? Nonce { get; init; }
    }
}