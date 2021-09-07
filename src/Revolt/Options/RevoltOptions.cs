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
        /// Default constructor.
        /// </summary>
        public RevoltOptions()
        {
            Endpoint = new Uri("https://api.revolt.chat/");
        }

        /// <summary>
        ///     Endpoint for the Revolt API.
        /// </summary>
        public Uri Endpoint { get; set; }
    }
}