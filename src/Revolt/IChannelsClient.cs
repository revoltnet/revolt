using System.Threading;
using System.Threading.Tasks;
using Revolt.Models.Channels.Groups;
using Revolt.Models.Channels.Information;
using Revolt.Models.Channels.Messaging;
using Revolt.Models.Channels.Voice;
using Revolt.Models.Users.DirectMessaging;

namespace Revolt
{
    public interface IChannelsClient
    {
        #region Information

        /// <summary>
        /// Query and fetch channels on Revolt.
        /// </summary>
        /// <param name="channelId">Channel id.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns>
        /// An implementation of <see cref="IChannel"/>.
        /// <para>
        /// Either a <see cref="SavedMessages"/>, a <see cref="DirectMessage"/>, a <see cref="Group"/>,
        /// a <see cref="TextChannel"/> or a <see cref="VoiceChannel"/>.
        /// </para>
        /// </returns>
        Task<IChannel> FetchChannelAsync(string channelId, CancellationToken cancellationToken = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">
        ///     Channel name.
        ///     <para>[ 1 .. 32 ] characters.</para>
        /// </param>
        /// <param name="description">
        ///     Channel description.
        ///     <para>[ 0 .. 1024 ] characters.</para></param>
        /// <param name="iconId">Autumn file id.</param>
        /// <param name="nsfw">Whether this channel is not safe for work.</param>
        /// <param name="remove">Field to remove from channel object.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns></returns>
        Task EditChannelAsync(string? name, string? description, string iconId, bool? nsfw, ERemovableChannelInformation remove, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a server channel, leaves a group or closes a DM.
        /// </summary>
        /// <param name="channelId">Channel id.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns></returns>
        Task DeleteChannelAsync(string channelId, CancellationToken cancellationToken = default);

        #endregion

        #region Invites

        /// <summary>
        /// Creates an invite to this channel. The channel Must be a <see cref="TextChannel"/>.
        /// </summary>
        /// <param name="channelId">Channel id.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns>An invite code.</returns>
        Task<string> CreateInviteAsync(string channelId, CancellationToken cancellationToken = default);

        #endregion

        #region Permissions

        /// <summary>
        /// Sets permissions for the specified role in this channel.
        /// <para>
        /// Channel must be a <see cref="TextChannel"/> or a <see cref="VoiceChannel"/>.
        /// </para>
        /// </summary>
        /// <param name="channelId">Channel id.</param>
        /// <param name="roleId">Role id.</param>
        /// <param name="permissions">Channel permissions.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns></returns>
        Task SetRolePermissionAsync(string channelId, string roleId, int permissions, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sets permissions for the default role in this channel.
        /// <para>
        /// Channel must be a <see cref="Group"/>, a <see cref="TextChannel"/> or a <see cref="VoiceChannel"/>.
        /// </para>
        /// </summary>
        /// <param name="channelId"></param>
        /// <param name="permissions"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task SetDefaultPermissionAsync(string channelId, int permissions, CancellationToken cancellationToken = default);

        #endregion
    }
}