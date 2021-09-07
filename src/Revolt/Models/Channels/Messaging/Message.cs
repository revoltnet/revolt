using System;
using System.Collections.Generic;
using Revolt.Models.Common;

namespace Revolt.Models.Channels.Messaging
{
    public class Message
    {
        public string Id { get; set; }
        public string Nonce { get; set; }
        public string Channel { get; set; }
        public string Author { get; set; }
        public IMessageContent MessageContent { get; set; }
        public List<File> Attachments { get; set; }
        public DateTimeOffset Edited { get; set; }
        public List<IEmbed> Embeds { get; set; }
        public List<string> Mentions { get; set; }
        public List<string> Replies { get; set; }
    }
}