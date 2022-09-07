using System.IO;

namespace SiteNews.Web.Models.Images
{
    public class ImageInputModel
    {
        public string FileName { get; set; }
        public string Type { get; set; }
        public Stream Content { get; set; }
    }
}
