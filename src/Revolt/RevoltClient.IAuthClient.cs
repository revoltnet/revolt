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
            var request = GenerateRequest(HttpMethod.Get, EAuth.Session, "auth/account");
            
            var response = await Client.SendAsync(request, cancellationToken).ConfigureAwait(false);

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
        public async Task<string> CreateAccountAsync(string email, string password, string? invite, string? captcha, CancellationToken cancellationToken = default)
        {
            var request = GenerateRequest(HttpMethod.Post, EAuth.None, "auth/account/create", new
            {
                email,
                password,
                invite,
                captcha
            });
            
            var response = await Client.SendAsync(request, cancellationToken).ConfigureAwait(false);

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
            var request = GenerateRequest(HttpMethod.Post, EAuth.None, "auth/account/reverify", new
            {
                email,
                captcha
            });
            
            var response = await Client.SendAsync(request, cancellationToken).ConfigureAwait(false);

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
            var request = GenerateRequest(HttpMethod.Post, EAuth.None, $"auth/account/verify/{code}");

            var response = await Client.SendAsync(request, cancellationToken).ConfigureAwait(false);

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
        public async Task SendPasswordResetAsync(string email, string captcha, CancellationToken cancellationToken = default)
        {
            var request = GenerateRequest(HttpMethod.Post, EAuth.None, "auth/account/reset_password", new
            {
                email,
                captcha
            });
            
            var response = await Client.SendAsync(request, cancellationToken).ConfigureAwait(false);

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
            var request = GenerateRequest(HttpMethod.Patch, EAuth.None, "auth/account/reset_password", new
            {
                password = newPassword,
                token
            });
            
            var response = await Client.SendAsync(request, cancellationToken).ConfigureAwait(false);

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
            var request = GenerateRequest(HttpMethod.Patch, EAuth.Session, "auth/account/change/password", new
            {
                password = oldPassword,
                new_password = newPassword
            });
            
            var response = await Client.SendAsync(request, cancellationToken).ConfigureAwait(false);

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
            var request = GenerateRequest(HttpMethod.Patch, EAuth.Session, "auth/account/change/email", new
            {
                password,
                new_email = newEmail
            });
            
            var response = await Client.SendAsync(request, cancellationToken).ConfigureAwait(false);

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
            var request = GenerateRequest(HttpMethod.Post, EAuth.None, "auth/login", new
            {
                email,
                password,
                device_name = deviceName,
                captcha
            });
            
            var response = await Client.SendAsync(request, cancellationToken).ConfigureAwait(false);

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
        public async Task LogoutAsync(CancellationToken cancellationToken)
        {
            var request = GenerateRequest(HttpMethod.Get, EAuth.Session, $"auth/logout");

            var response = await Client.SendAsync(request, cancellationToken).ConfigureAwait(false);

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
        public async Task EditSessionAsync(string sessionId, string friendlyName, CancellationToken cancellationToken = default)
        {
            var request = GenerateRequest(HttpMethod.Post, EAuth.Session, $"auth/sessions/{sessionId}", new
            {
                friendly_name = friendlyName
            });
            
            var response = await Client.SendAsync(request, cancellationToken).ConfigureAwait(false);
            
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
            var request = GenerateRequest(HttpMethod.Get, EAuth.Session, $"auth/sessions");

            var response = await Client.SendAsync(request, cancellationToken).ConfigureAwait(false);

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
        public async Task DeleteSessionAsync(string sessionId, CancellationToken cancellationToken = default)
        {
            var request = GenerateRequest(HttpMethod.Delete, EAuth.Session, $"auth/session/{sessionId}");

            var response = await Client.SendAsync(request, cancellationToken).ConfigureAwait(false);

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
        public async Task DeleteAllSessions(bool revokeSelf = false, CancellationToken cancellationToken = default)
        {
            var request = GenerateRequest(HttpMethod.Delete, EAuth.Session, $"auth/session/all?revoke_self={revokeSelf}");

            var response = await Client.SendAsync(request, cancellationToken).ConfigureAwait(false);

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