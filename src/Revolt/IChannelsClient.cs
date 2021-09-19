using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Revolt.Models.Channels.Groups;
using Revolt.Models.Channels.Information;
using Revolt.Models.Channels.Messaging;
using Revolt.Models.Channels.Voice;
using Revolt.Models.Users.DirectMessaging;
using Revolt.Models.Users.Information;

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
        /// <param name="channelId">Channel id.</param>
        /// <param name="permissions">TODO</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns></returns>
        Task SetDefaultPermissionAsync(string channelId, int permissions, CancellationToken cancellationToken = default);

        #endregion

        #region Messaging

        /// <summary>
        /// Sends a message to the given channel.
        /// </summary>
        /// <param name="channelId">Channel id.</param>
        /// <param name="content">
        /// Message content to send.
        /// <para>
        /// [ 0 .. 2000 ] characters.
        /// </para>
        /// </param>
        /// <param name="attachments">Attachments to include in message.</param>
        /// <param name="replies">
        /// Messages to reply to.
        /// <para>
        /// "mention" means whether this reply should mention the message's author.
        /// </para>
        /// </param>
        /// <param name="nonce">
        /// Nonce value, prefer to use GUIDs here for better feature support.
        /// Used to prevent double requests to create objects.
        /// <para>
        /// [ 1 .. 36 ] characters.
        /// </para>
        /// </param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns>The sent <see cref="Message"/></returns>
        Task<Message> SendMessageAsync(string channelId, string content, IEnumerable<string>? attachments,
            IEnumerable<(string messageId, bool mention)>? replies, string? nonce = default, CancellationToken cancellationToken = default);

        /// <summary>
        /// Fetches multiple messages.
        /// </summary>
        /// <param name="channelId">Channel id.</param>
        /// <param name="limit">
        /// Maximum number of messages to fetch.
        /// For fetching nearby messages, this is (limit + 1).
        /// <para>
        /// [ 1 .. 100 ]
        /// </para>
        /// </param>
        /// <param name="before">Message id before which messages should be fetched.</param>
        /// <param name="after">Message id after which messages should be fetched.</param>
        /// <param name="sort">Message sort direction.</param>
        /// <param name="nearby">Message id to fetch around, this will ignore 'before', 'after' and 'sort' options.
        /// Limits in each direction will be half of the specified limit.
        /// It also fetches the specified message ID.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns>A collection of <see cref="Message"/>.</returns>
        Task<IEnumerable<Message>> FetchMessagesAsync(string channelId, int limit, string before, string after, ESort sort, string nearby,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Fetches multiple messages and includes any related users (and members in channels).
        /// </summary>
        /// <param name="channelId">Channel id.</param>
        /// <param name="limit">
        /// Maximum number of messages to fetch.
        /// For fetching nearby messages, this is (limit + 1).
        /// <para>
        /// [ 1 .. 100 ]
        /// </para>
        /// </param>
        /// <param name="before">Message id before which messages should be fetched.</param>
        /// <param name="after">Message id after which messages should be fetched.</param>
        /// <param name="sort">Message sort direction.</param>
        /// <param name="nearby">Message id to fetch around, this will ignore 'before', 'after' and 'sort' options.
        /// Limits in each direction will be half of the specified limit.
        /// It also fetches the specified message ID.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns>
        /// A tuple containing a collection of <see cref="Message"/>, a collection of <see cref="User"/>,
        /// and in some cases a collection of <see cref="Member"/>.
        /// </returns>
        Task<(IEnumerable<Message> messages, IEnumerable<User> users, IEnumerable<Member>? members)> FetchMessagesWithUsersAsync(string channelId, 
            int limit, string before, string after, ESort sort, string nearby, CancellationToken cancellationToken = default);
        
        #endregion

        #region Voice

        /// <summary>
        /// Asks the voice server for a token to join the call.
        /// </summary>
        /// <param name="channelId">Channel id.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns>A Voso token.</returns>
        Task<string> JoinCallAsync(string channelId, CancellationToken cancellationToken = default);

        #endregion
    }
}