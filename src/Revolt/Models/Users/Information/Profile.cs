using System.Text.Json.Serialization;
using Revolt.Models.Common;

namespace Revolt.Models.Users.Information
{
    public class Profile
    {
        /// <summary>
        /// Profile content.
        /// </summary>
        [JsonPropertyName("content")]
        public string Content { get; set; }
        
        /// <summary>
        /// Profile background.
        /// </summary>
        [JsonPropertyName("background")]
        public File Background { get; set; }
    }
}