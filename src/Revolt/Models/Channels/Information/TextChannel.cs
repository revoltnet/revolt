#nullable enable
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Revolt.Models.Common;

namespace Revolt.Models.Channels.Information
{
    /// <summary>
    /// Text channel.
    /// </summary>
    public class TextChannel : IChannel
    {
        /// <inheritdoc />
        [JsonPropertyName("_id")]
        public string Id { get; init; } = null!;
        
        /// <summary>
        /// Group name.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; init; } = null!;

        /// <summary>
        /// Group description.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; init; }
        
        /// <summary>
        /// Group icon.
        /// </summary>
        [JsonPropertyName("icon")]
        public File? Icon { get; init; }
        
        /// <summary>
        /// Whether this channel is marked as not safe for work.
        /// </summary>
        [JsonPropertyName("nsfw")]
        public bool Nsfw { get; init; }

        /// <summary>
        /// Id of the last message in this channel.
        /// </summary>
        [JsonPropertyName("last_message_id")]
        public string? LastMessageId { get; init; }

        /// <summary>
        /// Permissions given to all users.
        /// </summary>
        [JsonPropertyName("default_permissions")]
        public int DefaultPermissions { get; init; }
        
        /// <summary>
        /// Permissions given to roles.
        /// </summary>
        [JsonPropertyName("role_permissions")]
        public IEnumerable<(string, int)>? RolePermissions { get; init; }

        /// <inheritdoc />
        [JsonPropertyName("nonce")]
        public string? Nonce { get; init; } = null!;
    }
}