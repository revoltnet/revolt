#nullable enable
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Revolt.Models.Channels.Information;
using Revolt.Models.Common;

namespace Revolt.Models.Channels.Groups
{
    /// <summary>
    /// Group.
    /// </summary>
    public class Group : IChannel
    {
        /// <inheritdoc />
        [JsonPropertyName("_id")]
        public string Id { get; init; } = null!;

        /// <summary>
        /// List of user IDs who are participating in this group.
        /// </summary>
        [JsonPropertyName("recipients")]
        public IEnumerable<string> Recipients { get; init; } = null!;

        /// <summary>
        /// Group name.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; init; } = null!;

        /// <summary>
        /// User ID of group owner.
        /// </summary>
        [JsonPropertyName("owner")]
        public string Owner { get; init; } = null!;
        
        /// <summary>
        /// Group description.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; init; }
        
        /// <summary>
        /// Id of the last message in this channel.
        /// </summary>
        [JsonPropertyName("last_message_id")]
        public string? LastMessageId { get; init; }
        
        /// <summary>
        /// Group icon.
        /// </summary>
        [JsonPropertyName("icon")]
        public File? Icon { get; init; }
        
        /// <summary>
        /// Permissions given to group members.
        /// </summary>
        [JsonPropertyName("permissions")]
        public int? Permissions { get; init; }
        
        /// <summary>
        /// Whether this channel is marked as not safe for work.
        /// </summary>
        [JsonPropertyName("nsfw")]
        public bool Nsfw { get; init; }

        /// <inheritdoc />
        [JsonPropertyName("nonce")]
        public string? Nonce { get; init; }
    }
}