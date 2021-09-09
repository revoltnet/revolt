using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Revolt.Models.Auth;
using Revolt.Models.Platform.Core;
using Revolt.Options;

namespace Revolt
{
    /// <inheritdoc />
    public class RevoltClient : IRevoltClient
    {
        /// <summary>
        ///     Options.
        /// </summary>
        public readonly RevoltOptions Options;

        /// <summary>
        /// <see cref="HttpClient"/>.
        /// </summary>
        protected HttpClient Client { get; }

        public RevoltClient(IOptions<RevoltOptions> options, HttpClient client)
        {
            Options = options.Value;
            Client = client;

            Client.BaseAddress = Options.Endpoint;
        }

        #region IPLatformClient

        /// <inheritdoc />
        public async Task<Node> QueryNodeAsync(CancellationToken cancellationToken = default)
        {
            var response = await Client.GetAsync(string.Empty, cancellationToken).ConfigureAwait(false);

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                throw new RevoltException(response.ReasonPhrase, e);
            }

            var node = await response.Content.ReadFromJsonAsync<Node>(cancellationToken: cancellationToken).ConfigureAwait(false);

            return node ?? throw new RevoltException("Something went wrong deserializing the response.");
        }

        /// <inheritdoc />
        public async Task<bool> CheckOnboardingStatusAsync(CancellationToken cancellationToken = default)
        {
            var response = await Client.GetAsync("onboard/hello", cancellationToken).ConfigureAwait(false);

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                throw new RevoltException(response.ReasonPhrase, e);
            }

            var content = await response.Content.ReadFromJsonAsync<JsonElement>(cancellationToken: cancellationToken).ConfigureAwait(false);

            if (content.TryGetProperty("onboarding", out content))
            {
                return content.GetBoolean();
            }

            throw new RevoltException("Something went wrong deserializing the response.");
        }

        /// <inheritdoc />
        public async Task CompleteOnboarding(string username, CancellationToken cancellationToken = default)
        {
            var payload = GeneratePayload(new
            {
                username
            });

            var response = await Client.PostAsync("onboard/complete", payload, cancellationToken);

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                throw new RevoltException(response.ReasonPhrase, e);
            }
        }

        #endregion

        #region IAuthClient

        /// <inheritdoc />
        public async Task<string> CreateAccountAsync(string email, string password, string invite, string captcha, CancellationToken cancellationToken = default)
        {
            var payload = GeneratePayload(new
            {
                email,
                password,
                invite,
                captcha
            });

            var response = await Client.PostAsync("auth/create", payload, cancellationToken);

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                throw new RevoltException(response.ReasonPhrase, e);
            }

            var content = await response.Content.ReadFromJsonAsync<JsonElement>(cancellationToken: cancellationToken).ConfigureAwait(false);

            if (content.TryGetProperty("user_id", out content))
            {
                return content.GetString() ?? throw new RevoltException("Something went wrong deserializing the response.");
            }

            throw new RevoltException("Something went wrong deserializing the response.");
        }
        /// <summary>
        /// Generates a valid <see cref="StringContent"/> payload.
        /// </summary>
        /// <param name="payload">The object for the body of the payload.</param>
        /// <typeparam name="T">A valid object.</typeparam>
        /// <returns>A valid <see cref="StringContent"/>.</returns>
        protected static StringContent GeneratePayload<T>(T payload)
        {
            return new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
        }
    }
}