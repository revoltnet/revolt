using System.Threading;
using System.Threading.Tasks;
using Revolt.Models.Platform.Core;

namespace Revolt
{
    public interface IPlatformClient
    {
        #region Core

        Task<Node> QueryNodeAsync(CancellationToken cancellationToken = default);

        #endregion

        #region Onboarding

        Task<bool> CheckOnboardingStatusAsync(CancellationToken cancellationToken = default);

        Task CompleteOnboarding(string username, CancellationToken cancellationToken = default);

        #endregion
    }
}