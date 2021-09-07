using Revolt.Models.Users.Information;

namespace Revolt.Models.Common
{
    public class File
    {
        public string Id { get; set; }
        public string Tag { get; set; }
        public long Size { get; set; }
        public string Filename { get; set; }
        public IMetadata Metadata { get; set; }
        public string ContentType { get; set; }
    }
}