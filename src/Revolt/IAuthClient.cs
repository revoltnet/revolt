using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Revolt.Models.Auth;

namespace Revolt
{
    public interface IAuthClient
    {
        /// <summary>
        /// Create a new account.
        /// </summary>
        /// <param name="email">Valid email address.</param>
        /// <param name="password">Password.</param>
        /// <param name="invite">Invite Code.</param>
        /// <param name="captcha">Captcha verification code.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns>The user id of the newly created user.</returns>
        public Task<string> CreateAccountAsync(string email, string password, string invite, string captcha,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Resend account creation verification email.
        /// </summary>
        /// <param name="email">Valid email address.</param>
        /// <param name="captcha">Captcha verification code.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns></returns>
        public Task ResendVerificationAsync(string email, string captcha, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Create a new session.
        /// </summary>
        /// <param name="email">Valid email address.</param>
        /// <param name="password">Password.</param>
        /// <param name="deviceName">Device name.</param>
        /// <param name="captcha">Captcha verification code.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns>A new and valid <see cref="Session"/>.</returns>
        public Task<Session> LoginAsync(string email, string password, string deviceName, string captcha,
            CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Send password reset email.
        /// </summary>
        /// <param name="email">Valid email address.</param>
        /// <param name="captcha">Captcha verification code.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns></returns>
        public Task SendPasswordResetAsync(string email, string captcha, CancellationToken cancellationToken = default);

        /// <summary>
        /// Confirm password reset.
        /// </summary>
        /// <param name="newPassword">New password.</param>
        /// <param name="token">Password reset token.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns></returns>
        public Task PasswordResetAsync(string newPassword, string token, CancellationToken cancellationToken = default);

        /// <summary>
        /// Fetch account information.
        /// </summary>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns>The information for the <see cref="Account"/>.</returns>
        public Task<Account> FetchAccountAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Check if we are authenticated.
        /// </summary>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns></returns>
        public Task CheckAuthAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Change account password.
        /// </summary>
        /// <param name="oldPassword">Old password.</param>
        /// <param name="newPassword">New password.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns></returns>
        public Task ChangePasswordAsync(string oldPassword, string newPassword, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Change account email.
        /// </summary>
        /// <param name="password">Password.</param>
        /// <param name="newEmail">New valid email address.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns></returns>
        public Task ChangeEmailAsync(string password, string newEmail, CancellationToken cancellationToken = default);

        /// <summary>
        /// Delete existing session.
        /// </summary>
        /// <param name="sessionId">Session id.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns></returns>
        public Task DeleteSessionAsync(string sessionId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Fetch all sessions.
        /// </summary>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns>A collection of <see cref="Session"/>.</returns>
        public Task<IEnumerable<Session>> FetchSessionsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Delete current session.
        /// </summary>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns></returns>
        public Task LogoutAsync(CancellationToken cancellationToken);
    }
}