using System.Text.Json.Serialization;

namespace Revolt.Models.Common
{
    /// <summary>
    ///     Represents a file, attachment, etc.
    /// </summary>
    public class File
    {
        /// <summary>
        ///     File id.
        /// </summary>
        [JsonPropertyName("_id")]
        public string Id { get; set; }

        /// <summary>
        ///     File tag.
        /// </summary>
        [JsonPropertyName("tag")]
        public string Tag { get; set; }

        /// <summary>
        ///     File size (in bytes)
        /// </summary>
        [JsonPropertyName("size")]
        public long Size { get; set; }

        /// <summary>
        ///     File name.
        /// </summary>
        [JsonPropertyName("filename")]
        public string Filename { get; set; }

        /// <summary>
        ///     File metadata.
        /// </summary>
        [JsonPropertyName("metadata")]
        public IMetadata Metadata { get; set; }

        /// <summary>
        ///     File content type.
        /// </summary>
        [JsonPropertyName("content_type")]
        public string ContentType { get; set; }
    }
}