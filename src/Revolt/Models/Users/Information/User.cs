using System.Collections.Generic;
using Revolt.Models.Common;
using Revolt.Models.Users.Relationships;

namespace Revolt.Models.Users.Information
{
    public class User
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public File Avatar { get; set; }
        public List<Relation> Relations { get; set; }
        public long Badges { get; set; }
        public UserStatus Status { get; set; }
        public ERelationship Relationship { get; set; }
        public bool Online { get; set; }
        public long Flags { get; set; }
        public BotInformation Bot { get; set; }

        public class BotInformation
        {
            public string Owner { get; set; }
        }

        public class UserStatus
        {
            public string Text { get; set; }
            public string Presence { get; set; }
        }
    }
}