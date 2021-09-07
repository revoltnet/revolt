using System.Text.Json.Serialization;

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
        [JsonPropertyName("revolt")]
        public string Revolt { get; }

        /// <summary>
        ///     Available features exposed by the API.
        /// </summary>
        [JsonPropertyName("features")]
        public NodeFeatures Features { get; }

        /// <summary>
        ///     WebSocket URL.
        /// </summary>
        [JsonPropertyName("ws")]
        public string Ws { get; }

        /// <summary>
        ///     URL to web app associated with this instance.
        /// </summary>
        [JsonPropertyName("app")]
        public string App { get; }

        /// <summary>
        ///     Web Push VAPID key.
        /// </summary>
        [JsonPropertyName("vapid")]
        public string Vapid { get; }

        /// <summary>
        ///     Available features exposed by the API.
        /// </summary>
        public class NodeFeatures
        {
            /// <summary>
            ///     Whether users can register.
            /// </summary>
            [JsonPropertyName("registration")]
            public bool Registration { get; }

            /// <summary>
            ///     hCaptcha options.
            /// </summary>
            [JsonPropertyName("captcha")]
            public CaptchaOptions Captcha { get; }

            /// <summary>
            ///     Whether email verification is enabled.
            /// </summary>
            [JsonPropertyName("email")]
            public bool Email { get; }

            /// <summary>
            ///     Whether an invite code is required to register.
            /// </summary>
            [JsonPropertyName("invite_only")]
            public string InviteOnly { get; }

            /// <summary>
            ///     Autumn (file server) options.
            /// </summary>
            [JsonPropertyName("autumn")]
            public AutumnOptions Autumn { get; }

            /// <summary>
            ///     January (proxy server) options.
            /// </summary>
            [JsonPropertyName("january")]
            public JanuaryOptions January { get; }

            /// <summary>
            ///     Legacy voice server options.
            /// </summary>
            [JsonPropertyName("voso")]
            public VosoOptions Voso { get; }


            /// <summary>
            ///     Autumn (file server) options.
            /// </summary>
            public class AutumnOptions
            {
                /// <summary>
                ///     Whether file uploads are enabled.
                /// </summary>
                [JsonPropertyName("enabled")]
                public bool Enabled { get; }

                /// <summary>
                ///     Autumn API URL.
                /// </summary>
                [JsonPropertyName("url")]
                public string Url { get; }
            }

            /// <summary>
            ///     January (proxy server) options.
            /// </summary>
            public class JanuaryOptions
            {
                /// <summary>
                ///     Whether link embeds are enabled.
                /// </summary>
                [JsonPropertyName("enabled")]
                public bool Enabled { get; }

                /// <summary>
                ///     January API URL.
                /// </summary>
                [JsonPropertyName("url")]
                public string Url { get; }
            }

            /// <summary>
            ///     hCaptcha options.
            /// </summary>
            public class CaptchaOptions
            {
                /// <summary>
                ///     Whether hCaptcha is enabled.
                /// </summary>
                [JsonPropertyName("enabled")]
                public bool Enabled { get; }

                /// <summary>
                ///     hCaptcha site key.
                /// </summary>
                [JsonPropertyName("key")]
                public string Key { get; }
            }

            /// <summary>
            ///     Legacy voice server options.
            /// </summary>
            public class VosoOptions
            {
                /// <summary>
                ///     Whether voice is available (using voso).
                /// </summary>
                [JsonPropertyName("enabled")]
                public bool Enabled { get; }

                /// <summary>
                ///     Voso API URL.
                /// </summary>
                [JsonPropertyName("url")]
                public string Url { get; }

                /// <summary>
                ///     Voso WebSocket URL.
                /// </summary>
                [JsonPropertyName("ws")]
                public string Ws { get; }
            }
        }
    }
}