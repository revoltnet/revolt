using System.Text.Json.Serialization;

namespace Revolt.Models.Auth
{
    public class Account
    {
        /// <summary>
        /// User ID.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; }
        
        /// <summary>
        /// User Email.
        /// </summary>
        [JsonPropertyName("email")]
        public string Email { get; }
    }
}