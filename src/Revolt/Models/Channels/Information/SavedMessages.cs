namespace Revolt.Models.Channels.Information
{
    public class SavedMessages : IChannel
    {
        public string Id { get; set; }
        public string User { get; set; }
        public string Nonce { get; set; }
    }
}