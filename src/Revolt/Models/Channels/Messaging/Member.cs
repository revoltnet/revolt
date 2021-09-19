using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Revolt.Models.Common;

namespace Revolt.Models.Channels.Messaging
{
    /// <summary>
    /// Channel member.
    /// </summary>
    public class Member
    {
        /// <summary>
        /// Identity of the member.
        /// </summary>
        [JsonPropertyName("id")]
        public MemberIdentity Id { get; init; } = null!;
        
        /// <summary>
        /// Nickname of the member.
        /// </summary>
        [JsonPropertyName("nickname")]
        public string? Nickname { get; init; }
        
        /// <summary>
        /// Avatar of the member.
        /// </summary>
        [JsonPropertyName("avatar")]
        public File? Avatar { get; init; }
        
        /// <summary>
        /// Roles of the member.
        /// </summary>
        [JsonPropertyName("roles")]
        public IEnumerable<string>? Roles { get; init; } 

        /// <summary>
        /// Member identity.
        /// </summary>
        public class MemberIdentity
        {
            /// <summary>
            /// Server.
            /// </summary>
            [JsonPropertyName("server")]
            public string Server { get; init; } = null!;
            
            /// <summary>
            /// User.
            /// </summary>
            [JsonPropertyName("user")]
            public string User { get; init; } = null!;
        }
    }
}