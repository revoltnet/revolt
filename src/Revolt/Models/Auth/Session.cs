using System.Text.Json.Serialization;

#nullable enable
namespace Revolt.Models.Auth
{
    /// <summary>
    ///     Session.
    /// </summary>
    public class Session
    {
        /// <summary>
        ///     Session ID.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; } = null!;

        /// <summary>
        ///     User ID. Optional.
        /// </summary>
        [JsonPropertyName("user_id")]
        public string? UserId { get; init; }

        /// <summary>
        ///     Session Token. Optional.
        /// </summary>
        [JsonPropertyName("session_token")]
        public string? SessionToken { get; init; }

        /// <summary>
        ///     Session device name.
        /// </summary>
        [JsonPropertyName("friendly_name")]
        public string? FriendlyName { get; init; }
    }
}