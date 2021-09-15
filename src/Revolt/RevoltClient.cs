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

        #region IAuthClient

        /// <inheritdoc />
        public async Task<string> CreateAccountAsync(string email, string password, string invite, string captcha,
            CancellationToken cancellationToken = default)
        {
            var payload = GeneratePayload(new
            {
                email,
                password,
                invite,
                captcha
            });

            var response = await Client.PostAsync("auth/create", payload, cancellationToken).ConfigureAwait(false);

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
                return content.GetString() ?? throw new RevoltException("Something went wrong deserializing the response.");

            throw new RevoltException("Something went wrong deserializing the response.");
        }

        /// <inheritdoc />
        public async Task ResendVerificationAsync(string email, string captcha, CancellationToken cancellationToken = default)
        {
            var payload = GeneratePayload(new
            {
                email,
                captcha
            });

            var response = await Client.PostAsync("auth/resend", payload, cancellationToken).ConfigureAwait(false);

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                throw new RevoltException(response.ReasonPhrase, e);
            }
        }

        /// <inheritdoc />
        public async Task<Session> LoginAsync(string email, string password, string deviceName, string captcha,
            CancellationToken cancellationToken = default)
        {
            var payload = GeneratePayload(new
            {
                email,
                password,
                device_name = deviceName,
                captcha
            });

            var response = await Client.PostAsync("auth/login", payload, cancellationToken).ConfigureAwait(false);

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                throw new RevoltException(response.ReasonPhrase, e);
            }

            var session = await response.Content.ReadFromJsonAsync<Session>(cancellationToken: cancellationToken).ConfigureAwait(false);

            return session ?? throw new RevoltException("Something went wrong deserializing the response.");
        }

        /// <inheritdoc />
        public async Task SendPasswordResetAsync(string email, string captcha, CancellationToken cancellationToken = default)
        {
            var payload = GeneratePayload(new
            {
                email,
                captcha
            });

            var response = await Client.PostAsync("auth/send_reset", payload, cancellationToken).ConfigureAwait(false);

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                throw new RevoltException(response.ReasonPhrase, e);
            }
        }

        /// <inheritdoc />
        public async Task PasswordResetAsync(string newPassword, string token, CancellationToken cancellationToken = default)
        {
            var payload = GeneratePayload(new
            {
                password = newPassword,
                token
            });

            var response = await Client.PostAsync("auth/reset", payload, cancellationToken).ConfigureAwait(false);

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                throw new RevoltException(response.ReasonPhrase, e);
            }
        }

        /// <inheritdoc />
        public async Task<Account> FetchAccountAsync(CancellationToken cancellationToken = default)
        {
            var response = await Client.GetAsync("auth/user", cancellationToken).ConfigureAwait(false);

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                throw new RevoltException(response.ReasonPhrase, e);
            }

            var account = await response.Content.ReadFromJsonAsync<Account>(cancellationToken: cancellationToken).ConfigureAwait(false);

            return account ?? throw new RevoltException("Something went wrong deserializing the response.");
        }

        /// <inheritdoc />
        public async Task CheckAuthAsync(CancellationToken cancellationToken = default)
        {
            var response = await Client.GetAsync("auth/check", cancellationToken).ConfigureAwait(false);

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                throw new RevoltException(response.ReasonPhrase, e);
            }
        }

        /// <inheritdoc />
        public async Task ChangePasswordAsync(string oldPassword, string newPassword, CancellationToken cancellationToken = default)
        {
            var payload = GeneratePayload(new
            {
                password = oldPassword,
                new_password = newPassword
            });

            var response = await Client.PostAsync("auth/change/password", payload, cancellationToken).ConfigureAwait(false);

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                throw new RevoltException(response.ReasonPhrase, e);
            }
        }

        /// <inheritdoc />
        public async Task ChangeEmailAsync(string password, string newEmail, CancellationToken cancellationToken = default)
        {
            var payload = GeneratePayload(new
            {
                password,
                new_email = newEmail
            });

            var response = await Client.PostAsync("auth/change/email", payload, cancellationToken).ConfigureAwait(false);

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                throw new RevoltException(response.ReasonPhrase, e);
            }
        }

        /// <inheritdoc />
        public async Task DeleteSessionAsync(string sessionId, CancellationToken cancellationToken = default)
        {
            var response = await Client.DeleteAsync($"auth/session/{sessionId}", cancellationToken).ConfigureAwait(false);

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                throw new RevoltException(response.ReasonPhrase, e);
            }
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Session>> FetchSessionsAsync(CancellationToken cancellationToken = default)
        {
            var response = await Client.GetAsync("auth/sessions", cancellationToken).ConfigureAwait(false);

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                throw new RevoltException(response.ReasonPhrase, e);
            }

            var sessions = await response.Content.ReadFromJsonAsync<IEnumerable<Session>>(cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            return sessions ?? throw new RevoltException("Something went wrong deserializing the response.");
        }

        /// <inheritdoc />
        public async Task LogoutAsync(CancellationToken cancellationToken)
        {
            var response = await Client.GetAsync("auth/logout", cancellationToken).ConfigureAwait(false);

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
    }
}