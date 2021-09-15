#nullable enable
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

        /// <summary>
        ///     Generates a valid <see cref="StringContent" /> payload.
        /// </summary>
        /// <param name="payload">The object for the body of the payload.</param>
        /// <typeparam name="T">A valid object.</typeparam>
        /// <returns>A valid <see cref="StringContent" />.</returns>
        protected static StringContent GeneratePayload<T>(T payload)
        {
            return new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
        }
    }
}