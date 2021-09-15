using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Revolt.Models.Channels.Information;
using Revolt.Models.Users.DirectMessaging;
using Revolt.Models.Users.Information;
using Revolt.Models.Users.Relationships;

namespace Revolt
{
    public partial class RevoltClient
    {
        #region IUsersClient

        /// <inheritdoc />
        public async Task<User> FetchUserAsync(string userId, CancellationToken cancellationToken = default)
        {
            var response = await Client.GetAsync($"users/{userId}", cancellationToken).ConfigureAwait(false);

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                throw new RevoltException(response.ReasonPhrase, e);
            }

            var user = await response.Content.ReadFromJsonAsync<User>(cancellationToken: cancellationToken).ConfigureAwait(false);

            return user ?? throw new RevoltException("Something went wrong deserializing the response.");
        }

        /// <inheritdoc />
        public async Task EditUserAsync(Status? status, Profile? profile, string? avatarId, ERemovableInformation? remove,
            CancellationToken cancellationToken = default)
        {
            var payload = GeneratePayload(new
            {
                status,
                profile,
                avatar = avatarId,
                remove = remove.ToString()
            });

            var response = await Client.PatchAsync("users/@me", payload, cancellationToken).ConfigureAwait(false);

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
        public async Task ChangeUsernameAsync(string newUsername, string password, CancellationToken cancellationToken = default)
        {
            var payload = GeneratePayload(new
            {
                username = newUsername,
                password
            });

            var response = await Client.PatchAsync("users/@me/username", payload, cancellationToken).ConfigureAwait(false);

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
        public async Task<Profile> FetchUserProfileAsync(string userId, CancellationToken cancellationToken = default)
        {
            var response = await Client.GetAsync($"users/{userId}/profile", cancellationToken).ConfigureAwait(false);

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                throw new RevoltException(response.ReasonPhrase, e);
            }

            var profile = await response.Content.ReadFromJsonAsync<Profile>(cancellationToken: cancellationToken).ConfigureAwait(false);

            return profile ?? throw new RevoltException("Something went wrong deserializing the response.");
        }

        /// <inheritdoc />
        public async Task<Stream> FetchDefaultAvatarAsync(string userId, CancellationToken cancellationToken = default)
        {
            var response = await Client.GetAsync($"users/{userId}/default_avatar", cancellationToken).ConfigureAwait(false);

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                throw new RevoltException(response.ReasonPhrase, e);
            }

            var avatar = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);

            return avatar ?? throw new RevoltException("Something went wrong deserializing the response.");
        }

        /// <inheritdoc />
        public async Task<IEnumerable<string>> FetchMutualFriendsAsync(string userId, CancellationToken cancellationToken = default)
        {
            var response = await Client.GetAsync($"users/{userId}/mutual", cancellationToken).ConfigureAwait(false);

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                throw new RevoltException(response.ReasonPhrase, e);
            }

            var mutualFriends = await response.Content.ReadFromJsonAsync<IEnumerable<string>>(cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            return mutualFriends ?? throw new RevoltException("Something went wrong deserializing the response.");
        }

        /// <inheritdoc />
        public async Task<IEnumerable<IChannel>> FetchDirectMessageChannelsAsync(CancellationToken cancellationToken = default)
        {
            var response = await Client.GetAsync($"users/dms", cancellationToken).ConfigureAwait(false);

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                throw new RevoltException(response.ReasonPhrase, e);
            }

            var content = await response.Content.ReadFromJsonAsync<JsonElement>(cancellationToken: cancellationToken).ConfigureAwait(false);

            if (content.ValueKind is not JsonValueKind.Array)
            {
                throw new RevoltException("TODO ");
            }

            var dms = new List<IChannel>();

            foreach (var elem in content.EnumerateArray())
            {
                if (elem.TryGetProperty("channel_type", out var channelType))
                {
                    if (channelType.GetString() == "DirectMessage")
                    {
                        var dm = JsonSerializer.Deserialize<DirectMessage>(elem.GetRawText());
                        if (dm is not null)
                        {
                            dms.Add(dm);
                        }
                    }
                    else if (channelType.GetString() == "Group")
                    {
                        //TODO
                    }
                }
            }

            return dms;
        }

        /// <inheritdoc />
        public async Task<DirectMessage> OpenDirectMessageAsync(string userId, CancellationToken cancellationToken = default)
        {
            var response = await Client.GetAsync($"users/{userId}/dm", cancellationToken).ConfigureAwait(false);

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                throw new RevoltException(response.ReasonPhrase, e);
            }

            var dm = await response.Content.ReadFromJsonAsync<DirectMessage>(cancellationToken: cancellationToken).ConfigureAwait(false);

            return dm ?? throw new RevoltException("Something went wrong deserializing the response.");
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Relationship>> FetchAllRelationshipsAsync(CancellationToken cancellationToken = default)
        {
            var response = await Client.GetAsync("users/relationships", cancellationToken).ConfigureAwait(false);

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                throw new RevoltException(response.ReasonPhrase, e);
            }

            var relationships = await response.Content.ReadFromJsonAsync<IEnumerable<Relationship>>(cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            return relationships ?? throw new RevoltException("Something went wrong deserializing the response.");
        }

        /// <inheritdoc />
        public async Task<Relationship> FetchRelationshipAsync(string userId, CancellationToken cancellationToken = default)
        {
            var response = await Client.GetAsync($"users/{userId}/relationships", cancellationToken).ConfigureAwait(false);

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                throw new RevoltException(response.ReasonPhrase, e);
            }

            var relationship = await response.Content.ReadFromJsonAsync<Relationship>(cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            return relationship ?? throw new RevoltException("Something went wrong deserializing the response.");
        }

        /// <inheritdoc />
        public async Task<ERelationship> SendFriendRequestAsync(string username, CancellationToken cancellationToken = default)
        {
            var response = await Client.PutAsync($"users/{username}/friend", null!, cancellationToken).ConfigureAwait(false);

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                throw new RevoltException(response.ReasonPhrase, e);
            }

            var content = await response.Content.ReadFromJsonAsync<JsonElement>(cancellationToken: cancellationToken).ConfigureAwait(false);

            if (content.TryGetProperty("status", out content) &&
                Enum.TryParse<ERelationship>(content.GetString(), out var status)) return status;

            throw new RevoltException("Something went wrong deserializing the response.");
        }

        /// <inheritdoc />
        public async Task<ERelationship> AcceptFriendRequestAsync(string username, CancellationToken cancellationToken = default)
        {
            return await SendFriendRequestAsync(username, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<ERelationship> DenyFriendRequestAsync(string username, CancellationToken cancellationToken = default)
        {
            var response = await Client.DeleteAsync($"users/{username}/friend", cancellationToken).ConfigureAwait(false);

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                throw new RevoltException(response.ReasonPhrase, e);
            }

            var content = await response.Content.ReadFromJsonAsync<JsonElement>(cancellationToken: cancellationToken).ConfigureAwait(false);

            if (content.TryGetProperty("status", out content) &&
                Enum.TryParse<ERelationship>(content.GetString(), out var status)) return status;

            throw new RevoltException("Something went wrong deserializing the response.");
        }

        /// <inheritdoc />
        public async Task<ERelationship> RemoveFriendAsync(string username, CancellationToken cancellationToken = default)
        {
            return await DenyFriendRequestAsync(username, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<ERelationship> BlockUserAsync(string userId, CancellationToken cancellationToken = default)
        {
            var response = await Client.PutAsync($"users/{userId}/block", null!, cancellationToken).ConfigureAwait(false);

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                throw new RevoltException(response.ReasonPhrase, e);
            }

            var content = await response.Content.ReadFromJsonAsync<JsonElement>(cancellationToken: cancellationToken).ConfigureAwait(false);

            if (content.TryGetProperty("status", out content) &&
                Enum.TryParse<ERelationship>(content.GetString(), out var status)) return status;

            throw new RevoltException("Something went wrong deserializing the response.");
        }

        /// <inheritdoc />
        public async Task<ERelationship> UnblockUserAsync(string userId, CancellationToken cancellationToken = default)
        {
            var response = await Client.DeleteAsync($"users/{userId}/friend", cancellationToken).ConfigureAwait(false);

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                throw new RevoltException(response.ReasonPhrase, e);
            }

            var content = await response.Content.ReadFromJsonAsync<JsonElement>(cancellationToken: cancellationToken).ConfigureAwait(false);

            if (content.TryGetProperty("status", out content) &&
                Enum.TryParse<ERelationship>(content.GetString(), out var status)) return status;

            throw new RevoltException("Something went wrong deserializing the response.");
        }

        #endregion
    }
}