using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Revolt.Models.Auth;

namespace Revolt
{
    public partial class RevoltClient
    {
        // This partial class implements all methods related to IAuthClient.

        #region Account

        /// <inheritdoc />
        public async Task<Account> FetchAccountAsync(CancellationToken cancellationToken = default)
        {
            var response = await Client.GetAsync("auth/account", cancellationToken).ConfigureAwait(false);

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
        public async Task<string> CreateAccountAsync(string email, string password, string invite, string captcha, CancellationToken cancellationToken = default)
        {
            var payload = GeneratePayload(new
            {
                email,
                password,
                invite,
                captcha
            });

            var response = await Client.PostAsync("auth/account/create", payload, cancellationToken).ConfigureAwait(false);

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

            var response = await Client.PostAsync("auth/account/reverify", payload, cancellationToken).ConfigureAwait(false);

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
        public async Task VerifyEmailAsync(string code, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public async Task SendPasswordResetAsync(string email, string captcha, CancellationToken cancellationToken = default)
        {
            var payload = GeneratePayload(new
            {
                email,
                captcha
            });

            var response = await Client.PostAsync("auth/account/reset_password", payload, cancellationToken).ConfigureAwait(false);

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

            var response = await Client.PatchAsync("auth/account/reset_password", payload, cancellationToken).ConfigureAwait(false);

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                throw new RevoltException(response.ReasonPhrase, e);
            }
        }

        public async Task ChangePasswordAsync(string oldPassword, string newPassword, CancellationToken cancellationToken = default)
        {
            var payload = GeneratePayload(new
            {
                password = oldPassword,
                new_password = newPassword
            });

            var response = await Client.PatchAsync("auth/account/change/password", payload, cancellationToken).ConfigureAwait(false);

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                throw new RevoltException(response.ReasonPhrase, e);
            }
        }

        public async Task ChangeEmailAsync(string password, string newEmail, CancellationToken cancellationToken = default)
        {
            var payload = GeneratePayload(new
            {
                password,
                new_email = newEmail
            });

            var response = await Client.PatchAsync("auth/account/change/email", payload, cancellationToken).ConfigureAwait(false);

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

        #region Session

        /// <inheritdoc />
        public async Task<Session> LoginAsync(string email, string password, string deviceName, string captcha, CancellationToken cancellationToken = default)
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

        public async Task EditSessionAsync(string sessionId, string friendlyName, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

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

        public async Task DeleteAllSessions(bool revokeSelf = false, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}