#nullable enable
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Revolt.Models.Channels.Information;
using Revolt.Models.Common;

namespace Revolt.Models.Channels.Voice
{
    /// <summary>
    /// Voice channel.
    /// </summary>
    public class VoiceChannel : IChannel
    {
        /// <inheritdoc />
        [JsonPropertyName("_id")]
        public string Id { get; init; } = null!;

        /// <summary>
        /// Server id.
        /// </summary>
        [JsonPropertyName("server")]
        public string Server { get; init; } = null!;
        
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