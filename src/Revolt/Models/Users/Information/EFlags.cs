using System;

namespace Revolt.Models.Users.Information
{
    /// <summary>
    /// User flags.
    /// </summary>
    [Flags]
    public enum EFlags
    {
        /// <summary>
        /// Account is suspended.
        /// </summary>
        Suspended = 1,
        
        /// <summary>
        /// Account was deleted.
        /// </summary>
        Deleted = 2,
        
        /// <summary>
        /// Account is banned.
        /// </summary>
        Banned = 4
    }
}