using System.Collections.Generic;
using SiteNews.Web.Models.Images;

namespace SiteNews.Web.Services
{
    public interface IImageService
    {
        public void Process(IEnumerable<ImageInputModel> images);
        public void ProcessGallery(IEnumerable<ImageInputModel> images);
    }
}