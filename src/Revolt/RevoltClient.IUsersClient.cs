#nullable enable
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
        // This partial class implements all methods related to IUsersClient.

        #region Information

        /// <inheritdoc />
        public async Task<User> FetchUserAsync(string userId, CancellationToken cancellationToken = default)
        {
            var request = GenerateRequest(HttpMethod.Get, EAuth.Priority, $"users/{userId}");

            var response = await Client.SendAsync(request, cancellationToken).ConfigureAwait(false);

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
        public async Task EditUserAsync(Status? status, Profile? profile, string? avatarId, ERemovableUserInformation? remove,
            CancellationToken cancellationToken = default)
        {
            var request = GenerateRequest(HttpMethod.Patch, EAuth.Priority, $"users/@me", new
            {
                status,
                profile,
                avatar = avatarId,
                remove = remove.ToString()
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
        public async Task ChangeUsernameAsync(string newUsername, string password, CancellationToken cancellationToken = default)
        {
            var request = GenerateRequest(HttpMethod.Patch, EAuth.Session, $"users/@me/username", new
            {
                username = newUsername,
                password
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
        public async Task<Profile> FetchUserProfileAsync(string userId, CancellationToken cancellationToken = default)
        {
            var request = GenerateRequest(HttpMethod.Get, EAuth.Priority, $"users/{userId}/profile");

            var response = await Client.SendAsync(request, cancellationToken).ConfigureAwait(false);

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
            var request = GenerateRequest(HttpMethod.Get, EAuth.None, $"users/{userId}/default_avatar");

            var response = await Client.SendAsync(request, cancellationToken).ConfigureAwait(false);

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
            
            var request = GenerateRequest(HttpMethod.Get, EAuth.Priority, $"users/{userId}/mutual");

            var response = await Client.SendAsync(request, cancellationToken).ConfigureAwait(false);

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

        #endregion

        #region Direct Messaging
        
        /// <inheritdoc />
        public async Task<IEnumerable<IChannel>> FetchDirectMessageChannelsAsync(CancellationToken cancellationToken = default)
        {
            var request = GenerateRequest(HttpMethod.Get, EAuth.Priority, $"users/dms");

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

            if (content.ValueKind is not JsonValueKind.Array)
            {
                throw new RevoltException("TODO ");
            }

            var dms = new List<IChannel>();

            foreach (var elem in content.EnumerateArray())
            {
                if (!elem.TryGetProperty("channel_type", out var channelType)) continue;
                
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

            return dms;
        }

        /// <inheritdoc />
        public async Task<DirectMessage> OpenDirectMessageAsync(string userId, CancellationToken cancellationToken = default)
        {
            var request = GenerateRequest(HttpMethod.Get, EAuth.Priority, $"users/{userId}/dm");

            var response = await Client.SendAsync(request, cancellationToken).ConfigureAwait(false);

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
        
        #endregion
        
        #region Relationships

        /// <inheritdoc />
        public async Task<IEnumerable<Relationship>> FetchAllRelationshipsAsync(CancellationToken cancellationToken = default)
        {
            var request = GenerateRequest(HttpMethod.Get, EAuth.Session, "users/relationships");

            var response = await Client.SendAsync(request, cancellationToken).ConfigureAwait(false);

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
            var request = GenerateRequest(HttpMethod.Get, EAuth.Session, $"users/{userId}/relationships");

            var response = await Client.SendAsync(request, cancellationToken).ConfigureAwait(false);

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
            var request = GenerateRequest(HttpMethod.Put, EAuth.Session, $"users/{username}/friend");

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
            var request = GenerateRequest(HttpMethod.Delete, EAuth.Session, $"users/{username}/friend");

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
            
            var request = GenerateRequest(HttpMethod.Put, EAuth.Session, $"users/{userId}/block");

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

            if (content.TryGetProperty("status", out content) &&
                Enum.TryParse<ERelationship>(content.GetString(), out var status)) return status;

            throw new RevoltException("Something went wrong deserializing the response.");
        }

        /// <inheritdoc />
        public async Task<ERelationship> UnblockUserAsync(string userId, CancellationToken cancellationToken = default)
        {
            var request = GenerateRequest(HttpMethod.Delete, EAuth.Session, $"users/{userId}/friend");

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

            if (content.TryGetProperty("status", out content) &&
                Enum.TryParse<ERelationship>(content.GetString(), out var status)) return status;

            throw new RevoltException("Something went wrong deserializing the response.");
        }

        #endregion
    }
}