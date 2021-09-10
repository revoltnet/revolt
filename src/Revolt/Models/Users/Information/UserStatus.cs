using System.Text.Json.Serialization;

namespace Revolt.Models.Users.Information
{
    /// <summary>
    /// User status.
    /// </summary>
    public class UserStatus
    {
        /// <summary>
        /// User's custom status text.
        /// </summary>
        [JsonPropertyName("text")]
        public string? Text { get; init; }
            
        /// <summary>
        /// User presence.
        /// </summary>
        [JsonPropertyName("presence")]
        public EPresence? Presence { get; init; }

        /// <summary>
        /// Possible user presences.
        /// </summary>
        public enum EPresence
        {
            /// <summary>
            /// Busy.
            /// </summary>
            Busy,
                
            /// <summary>
            /// Idle.
            /// </summary>
            Idle,
                
            /// <summary>
            /// Invisible.
            /// </summary>
            Invisible,
                
            /// <summary>
            /// Online.
            /// </summary>
            Online
        }
    }
}