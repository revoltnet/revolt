using System.Collections.Generic;
using Revolt.Models.Channels.Information;

namespace Revolt.Models.Users.DirectMessaging
{
  
    public class DirectMessage : IChannel
    {
        public string Id { get; }
        public bool Active { get; set; }
        public List<string> Recipients { get; set; }
        public LastMessageData LastMessage { get; set; }
        public string Nonce { get; }
    }
}