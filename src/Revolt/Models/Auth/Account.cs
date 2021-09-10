using System.Text.Json.Serialization;

namespace Revolt.Models.Auth
{
    /// <summary>
    /// User account.
    /// </summary>
    public class Account
    {
        /// <summary>
        /// User ID.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; init; }
        
        /// <summary>
        /// User Email.
        /// </summary>
        [JsonPropertyName("email")]
        public string Email { get; init; }
    }
}