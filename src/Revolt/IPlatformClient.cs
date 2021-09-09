using System.Threading;
using System.Threading.Tasks;
using Revolt.Models.Platform.Core;

namespace Revolt
{
    public interface IPlatformClient
    {
        #region Core

        /// <summary>
        /// This returns information about which features are enabled on the remote node.
        /// </summary>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns>All <see cref="Node"/> information.</returns>
        Task<Node> QueryNodeAsync(CancellationToken cancellationToken = default);

        #endregion

        #region Onboarding

        /// <summary>
        /// This will tell you whether the current account requires onboarding or whether you can continue to send requests as usual.
        /// </summary>
        /// <remarks>
        /// You may skip calling this if you're restoring an existing session.
        /// </remarks>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns>True if the account requires onboarding, false otherwise.</returns>
        Task<bool> CheckOnboardingStatusAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// This sets a new username, completes onboarding and allows a user to start using Revolt.
        /// </summary>
        /// <param name="username">New username.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns></returns>
        Task CompleteOnboarding(string username, CancellationToken cancellationToken = default);

        #endregion
    }
}