#nullable enable
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Revolt.Models.Common;
using Revolt.Models.Users.Relationships;

namespace Revolt.Models.Users.Information
{
    /// <summary>
    /// User.
    /// </summary>
    public class User
    {
        /// <summary>
        /// User id.
        /// </summary>
        [JsonPropertyName("_id")]
        public string Id { get; init; } = null!;

        /// <summary>
        /// Username.
        /// </summary>
        [JsonPropertyName("username")]
        public string Username { get; init; } = null!;
        
        /// <summary>
        /// User avatar.
        /// </summary>
        [JsonPropertyName("avatar")]
        public File? Avatar { get; init; }
        
        /// <summary>
        /// Relationships with other known users.
        /// </summary>
        /// <remarks>
        /// Only present if fetching self.
        /// </remarks>
        [JsonPropertyName("relations")]
        public List<Relationship>? Relations { get; init; }
        
        /// <summary>
        /// Bitfield of user's badges.
        /// </summary>
        [JsonPropertyName("badges")]
        public long? Badges { get; init; }
        
        /// <summary>
        /// User status.
        /// </summary>
        [JsonPropertyName("status")]
        public Status? Status { get; init; }

        /// <summary>
        /// Your relationship with the user.
        /// </summary>
        /// <remarks>
        /// Defaults to none.
        /// </remarks>
        [JsonPropertyName("relationship")]
        public ERelationship Relationship { get; init; } = ERelationship.None;
        
        /// <summary>
        /// Whether the user is online.
        /// </summary>
        [JsonPropertyName("online")]
        public bool Online { get; init; }
        
        /// <summary>
        /// User flags.
        /// </summary>
        [JsonPropertyName("flags")]
        public long? Flags { get; init; }
        
        /// <summary>
        /// Bot information.
        /// </summary>
        /// <remarks>
        /// Only present if fetching a bot, or self is a bot.
        /// </remarks>
        [JsonPropertyName("bot")]
        public BotInformation? Bot { get; init; }

        /// <summary>
        /// Bot information.
        /// </summary>
        public class BotInformation
        {
            /// <summary>
            /// The user id of the owner of the bot.
            /// </summary>
            [JsonPropertyName("owner")]
            public string Owner { get; init; } = null!;
        }
    }
}