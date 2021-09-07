namespace Revolt.Models.Platform.Core
{
    /// <summary>
    ///     Information about which features are enabled on the remote node.
    /// </summary>
    public class Node
    {
        /// <summary>
        ///     Revolt API version string.
        /// </summary>
        public string Revolt { get; set; }

        /// <summary>
        ///     Available features exposed by the API.
        /// </summary>
        public NodeFeatures Features { get; set; }

        /// <summary>
        ///     WebSocket URL.
        /// </summary>
        public string Ws { get; set; }

        /// <summary>
        ///     URL to web app associated with this instance.
        /// </summary>
        public string App { get; set; }

        /// <summary>
        ///     Web Push VAPID key.
        /// </summary>
        public string Vapid { get; set; }

        /// <summary>
        ///     Available features exposed by the API.
        /// </summary>
        public class NodeFeatures
        {
            /// <summary>
            ///     Whether users can register.
            /// </summary>
            public bool Registration { get; set; }

            /// <summary>
            ///     hCaptcha options.
            /// </summary>
            public CaptchaOptions Captcha { get; set; }

            /// <summary>
            ///     Whether email verification is enabled.
            /// </summary>
            public bool Email { get; set; }

            /// <summary>
            ///     Whether an invite code is required to register.
            /// </summary>
            public string InviteOnly { get; set; }

            /// <summary>
            ///     Autumn (file server) options.
            /// </summary>
            public AutumnOptions Autumn { get; set; }

            /// <summary>
            ///     January (proxy server) options.
            /// </summary>
            public JanuaryOptions January { get; set; }

            /// <summary>
            ///     Legacy voice server options.
            /// </summary>
            public VosoOptions Voso { get; set; }


            /// <summary>
            ///     Autumn (file server) options.
            /// </summary>
            public class AutumnOptions
            {
                /// <summary>
                ///     Whether file uploads are enabled.
                /// </summary>
                public bool Enabled { get; set; }

                /// <summary>
                ///     Autumn API URL.
                /// </summary>
                public string Url { get; set; }
            }

            /// <summary>
            ///     January (proxy server) options.
            /// </summary>
            public class JanuaryOptions
            {
                /// <summary>
                ///     Whether link embeds are enabled.
                /// </summary>
                public bool Enabled { get; set; }

                /// <summary>
                ///     January API URL.
                /// </summary>
                public string Url { get; set; }
            }

            /// <summary>
            ///     hCaptcha options.
            /// </summary>
            public class CaptchaOptions
            {
                /// <summary>
                ///     Whether hCaptcha is enabled.
                /// </summary>
                public bool Enabled { get; set; }

                /// <summary>
                ///     hCaptcha site key.
                /// </summary>
                public string Key { get; set; }
            }

            /// <summary>
            ///     Legacy voice server options.
            /// </summary>
            public class VosoOptions
            {
                /// <summary>
                ///     Whether voice is available (using voso).
                /// </summary>
                public bool Enabled { get; set; }

                /// <summary>
                ///     Voso API URL.
                /// </summary>
                public string Url { get; set; }

                /// <summary>
                ///     Voso WebSocket URL.
                /// </summary>
                public string Ws { get; set; }
            }
        }
    }
}