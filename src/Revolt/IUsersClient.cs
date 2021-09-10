using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Revolt.Models.Channels.Information;
using Revolt.Models.Users.DirectMessaging;
using Revolt.Models.Users.Information;
using Revolt.Models.Users.Relationships;

namespace Revolt
{
    public interface IUsersClient
    {
        #region Information

        /// <summary>
        /// Retrieve a user's information.
        /// </summary>
        /// <param name="userId">User id.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>An instance of <see cref="User"/>.</returns>
        Task<User> FetchUser(string userId, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Edit your user object.
        /// </summary>
        /// <param name="status">User status.</param>
        /// <param name="profile">User profile data.</param>
        /// <param name="avatarId">Autumn file ID.</param>
        /// <param name="remove">Field to remove from user object.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task EditUser(UserStatus status, Profile profile, string avatarId, ERemovableInformation remove, CancellationToken cancellationToken = default);

        /// <summary>
        /// Change your username.
        /// </summary>
        /// <param name="newUsername">New username.</param>
        /// <param name="password">Current account password.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task ChangeUsername(string newUsername, string password, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieve a user's profile data.
        /// </summary>
        /// <param name="userId">User id.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>An instance of <see cref="Profile"/>.</returns>
        Task<Profile> FetchUserProfile(string userId, CancellationToken cancellationToken = default);

        /// <summary>
        /// This returns a default avatar based on the given id.
        /// </summary>
        /// <param name="userId">User id.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Default avatar in PNG format.</returns>
        Task<MemoryStream> FetchDefaultAvatar(string userId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieve a list of mutual friends with another user.
        /// </summary>
        /// <param name="userId">User id.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Collection of user IDs who both you and the other user are friends with.</returns>
        Task<IEnumerable<string>> FetchMutualFriends(string userId, CancellationToken cancellationToken = default);

        #endregion

        #region DirectMessaging

        /// <summary>
        /// This fetches your direct messages, including any DM and group DM conversations.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<IChannel>> FetchDirectMessageChannels(CancellationToken cancellationToken = default);

        Task<DirectMessage> OpenDirectMessage(string userId, CancellationToken cancellationToken = default);

        #endregion

        #region Relationships

        Task<IEnumerable<Relationship>> FetchAllRelationships(CancellationToken cancellationToken = default);

        Task<Relationship> FetchRelationship(CancellationToken cancellationToken = default);

        Task<UserStatus> SendFriendRequest(string username, CancellationToken cancellationToken = default);
        
        Task<UserStatus> AcceptFriendRequest(string username, CancellationToken cancellationToken = default);

        Task<UserStatus> DenyFriendRequest(string username, CancellationToken cancellationToken = default);

        Task<UserStatus> RemoveFriend(string username, CancellationToken cancellationToken = default);
        
        Task<UserStatus> BlockUser(string userId, CancellationToken cancellationToken = default);
        
        Task<UserStatus> UnblockUser(string userId, CancellationToken cancellationToken = default);

        #endregion
    }
}