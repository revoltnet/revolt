using System.Text.Json.Serialization;

namespace Revolt.Models.Channels.Information
{
    /// <summary>
    /// Last message sent in channel.
    /// </summary>
    public class LastMessage
    {
        /// <summary>
        /// Message id.
        /// </summary>
        [JsonPropertyName("_id")]
        public string Id { get; set; }
        
        /// <summary>
        /// Author id.
        /// </summary>
        [JsonPropertyName("author")]
        public string Author { get; set; }
        
        /// <summary>
        /// Short content of message.
        /// </summary>
        /// <remarks>
        /// ≥ 128 characters.
        /// </remarks>
        [JsonPropertyName("short")]
        public string Short { get; set; }
    }
}