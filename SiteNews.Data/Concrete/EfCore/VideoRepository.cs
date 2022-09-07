using Microsoft.EntityFrameworkCore;
using SiteNews.Data.Abstract;
using SiteNews.Entity;

namespace SiteNews.Data.Concrete.EfCore
{
    public class VideoRepository : GenericRepository<Video>, IVideoRepository
    {
        public VideoRepository(DbContext context) : base(context)
        {
        }
    }
}
