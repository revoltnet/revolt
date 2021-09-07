#nullable enable
namespace Revolt.Models.Auth
{
    /// <summary>
    /// Session.
    /// </summary>
    public class Session
    {
        /// <summary>
        /// Session ID.
        /// </summary>
        public string Id { get; set; } = null!;

        /// <summary>
        /// User ID. Optional.
        /// </summary>
        public string? UserId { get; set; }

        /// <summary>
        /// Session Token. Optional.
        /// </summary>
        public string? SessionToken { get; set; }

        /// <summary>
        /// Session device name.
        /// </summary>
        public string FriendlyName { get; set; } = null!;
    }
}