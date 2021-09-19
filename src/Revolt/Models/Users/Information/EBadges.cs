using System;

namespace Revolt.Models.Users.Information
{
    /// <summary>
    /// Bitfield of user's badges.
    /// </summary>
    [Flags]
    public enum EBadges
    {
        /// <summary>
        /// Developer.
        /// </summary>
        Developer = 1,
        
        /// <summary>
        /// Translator.
        /// </summary>
        Translator = 2,
        
        /// <summary>
        /// Supporter.
        /// </summary>
        Supporter = 4,
        
        /// <summary>
        /// Responsible Disclosure.
        /// </summary>
        ResponsibleDisclosure = 8,
        
        /// <summary>
        /// Revolt team.
        /// </summary>
        RevoltTeam = 16,
        
        /// <summary>
        /// Early adopter.
        /// </summary>
        EarlyAdopter = 256
    }
}