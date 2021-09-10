using System.Text.Json.Serialization;

namespace Revolt.Models.Users.Relationships
{
    /// <summary>
    /// Represents a relationship with a user.
    /// </summary>
    public class Relationship
    {
        /// <summary>
        /// Your relationship with the user.
        /// </summary>
        [JsonPropertyName("status")]
        public ERelationship Status { get; set; }
        
        /// <summary>
        /// Other user's id.
        /// </summary>
        [JsonPropertyName("_id")]
        public string Id { get; set; }
    }
}