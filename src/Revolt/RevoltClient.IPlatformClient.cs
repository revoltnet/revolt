using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Revolt.Models.Platform.Core;

namespace Revolt
{
    public partial class RevoltClient
    {
        // This partial class implements all methods related to IPlatformClient.

        #region Core

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

        #endregion

        #region Onboarding
        
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

            if (content.TryGetProperty("onboarding", out content)) return content.GetBoolean();

            throw new RevoltException("Something went wrong deserializing the response.");
        }

        /// <inheritdoc />
        public async Task CompleteOnboardingAsync(string username, CancellationToken cancellationToken = default)
        {
            var payload = GeneratePayload(new
            {
                username
            });

            var response = await Client.PostAsync("onboard/complete", payload, cancellationToken).ConfigureAwait(false);

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