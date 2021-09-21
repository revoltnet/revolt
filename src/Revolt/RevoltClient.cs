using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Revolt.Models.Auth;
using Revolt.Models.Channels.Information;
using Revolt.Models.Platform.Core;
using Revolt.Models.Users.DirectMessaging;
using Revolt.Models.Users.Information;
using Revolt.Models.Users.Relationships;
using Revolt.Options;

namespace Revolt
{
    /// <inheritdoc />
    public partial class RevoltClient : IRevoltClient
    {
        /// <summary>
        ///     Options.
        /// </summary>
        public readonly RevoltOptions Options;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="client"></param>
        public RevoltClient(IOptions<RevoltOptions> options, HttpClient client)
        {
            Options = options.Value;
            Client = client;

            Client.BaseAddress = Options.Endpoint;
        }

        /// <summary>
        ///     <see cref="HttpClient" />.
        /// </summary>
        protected HttpClient Client { get; }

        protected HttpRequestMessage GenerateRequest<TPayload>(HttpMethod method, EAuth auth, string? uri = default,
            TPayload? content = default)
        {
            return GenerateRequest(method, auth, uri, GeneratePayload(content));
            
            
            static StringContent? GeneratePayload<T>(T? payload)
            {
                return payload is not null
                    ? new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json")
                    : default;

            }
        }

        /// <summary>
        ///     Generates a request.
        /// </summary>
        /// <param name="method"></param>
        /// <param name="auth"></param>
        /// <param name="uri"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        protected HttpRequestMessage GenerateRequest(HttpMethod method, EAuth auth, string? uri = default, HttpContent? content = default)
        {
            var request = new HttpRequestMessage(method, uri)
            {
                Content = content
            };

            switch (auth)
            {
                case EAuth.Priority:
                    if (Options.PreferBotAuthentication is true)
                        UseBotAuthentication();
                    else
                        UseSessionAuthentication();
                    break;
                case EAuth.Session:
                    UseSessionAuthentication();
                    break;
                case EAuth.Bot:
                    UseBotAuthentication();
                    break;
                case EAuth.None:
                default:
                    break;
            }

            return request;

            void UseSessionAuthentication()
            {
                request.Headers.Add("x-session-token", Options.SessionToken);
            }

            void UseBotAuthentication()
            {
                request.Headers.Add("x-bot-token", Options.BotToken);
            }
        }
        
        /// <summary>
        /// Revolt request auth types.
        /// </summary>
        protected enum EAuth
        {
            /// <summary>
            /// None. No auth required.
            /// </summary>
            None,
            
            /// <summary>
            /// Session. Session token required.
            /// </summary>
            Session,
            
            /// <summary>
            /// Bot. Bot token required.
            /// </summary>
            Bot,
            
            /// <summary>
            /// Priority. Depends on configured priority.
            /// <remarks>
            /// Defaults to session authentication.
            /// </remarks>
            /// </summary>
            Priority
        }
    }
}