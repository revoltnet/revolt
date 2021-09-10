namespace Revolt.Models.Users.Relationships
{
    /// <summary>
    ///     Possible user relationships.
    /// </summary>
    public enum ERelationship
    {
        /// <summary>
        ///     Blocked.
        /// </summary>
        Blocked,

        /// <summary>
        ///     Blocked other.
        /// </summary>
        BlockedOther,

        /// <summary>
        ///     Friend.
        /// </summary>
        Friend,

        /// <summary>
        ///     Incoming.
        /// </summary>
        Incoming,

        /// <summary>
        ///     None.
        /// </summary>
        None,

        /// <summary>
        ///     Outgoing.
        /// </summary>
        Outgoing,

        /// <summary>
        ///     User.
        /// </summary>
        User
    }
}