namespace Revolt
{
    /// <summary>
    ///     Fully featured Revolt API client.
    ///     Includes implementations for platform API, auth API.
    /// </summary>
    /// <remarks>
    ///     See also <seealso cref="IPlatformClient" />.
    ///     See also <seealso cref="IAuthClient" />.
    /// </remarks>
    public interface IRevoltClient : IPlatformClient, IAuthClient, IUsersClient
    {
    }
}