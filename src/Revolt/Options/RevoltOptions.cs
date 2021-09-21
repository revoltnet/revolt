using System;

namespace Revolt.Options
{
    /// <summary>
    ///     Options to configure the Revolt client.
    /// </summary>
    public class RevoltOptions
    {
        /// <summary>
        ///     Name of the section in the settings.
        /// </summary>
        public const string Revolt = nameof(Revolt);

        /// <summary>
        ///     Default constructor.
        /// </summary>
        public RevoltOptions()
        {
            Endpoint = new Uri("https://api.revolt.chat/");
        }

        /// <summary>
        ///     Endpoint for the Revolt API.
        /// </summary>
        public Uri Endpoint { get; set; }
        
        /// <summary>
        ///     Session Token.
        /// </summary>
        public string? SessionToken { get; set; }
        
        /// <summary>
        ///     Bot Token.
        /// </summary>
        public string? BotToken { get; set; }
        
        /// <summary>
        ///     Whether to prioritize bot authentication over session authentication.
        ///     <remarks>
        ///         Useful when both tokens are provided but the use case is primarily bot-like.
        ///     </remarks> 
        /// </summary>
        public bool PreferBotAuthentication { get; set; }
    }
}