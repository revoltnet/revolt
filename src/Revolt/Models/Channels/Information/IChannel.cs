using Revolt.Models.Users.Information;

namespace Revolt.Models.Channels.Information
{
    public interface IChannel
    {
        string Id { get; }
        string Nonce { get; }
    }
}